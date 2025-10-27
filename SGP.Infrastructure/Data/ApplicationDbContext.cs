using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SGP.Core.Entities.Items;
using SGP.Infrastructure.Authentication;
using System;
using System.Linq.Expressions;

namespace SGP.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ItemCategory> itemCategories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemLot> ItemLots { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");


            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


            StringifyEnums(builder);
            SetUpLogicalDelete(builder);

            //Configure Defualt Decimal
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("decimal(20, 5)");
            } 
        }

        private void StringifyEnums(ModelBuilder builder)
        {
            var modelTypes = typeof(ApplicationDbContext).GetProperties()
                                         .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                                         .Select(x => x.PropertyType.GetGenericArguments().First())
                                         .ToList();

            foreach (Type modelType in modelTypes)
            {
                var properties = modelType.GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        builder.Entity(modelType).Property(property.Name).HasColumnType("varchar(50)").HasConversion<string>();
                        continue;
                    }
                }
            }
        }

        private void SetUpLogicalDelete(ModelBuilder builder)
        {
            //Logical Delete
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.AddProperty("IsDeleted", typeof(bool));
                var parameter = Expression.Parameter(entityType.ClrType);
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda(compareExpression, parameter);
                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        new public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true; 
                        foreach (var navigationEntry in entry.Navigations.Where(x => !((IReadOnlyNavigation)x.Metadata).IsOnDependent))
                        {
                            if (navigationEntry is CollectionEntry collectionEntry)
                            {
                                foreach (var dependentEntry in collectionEntry.CurrentValue)
                                {
                                    HandleDependent(Entry(dependentEntry));
                                }
                            }
                            else
                            {
                                var dependentEntry = navigationEntry.CurrentValue;
                                if (dependentEntry != null)
                                {
                                    HandleDependent(Entry(dependentEntry));
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void HandleDependent(EntityEntry entry)
        {
            entry.CurrentValues["IsDeleted"] = true;
        }
    }
}
