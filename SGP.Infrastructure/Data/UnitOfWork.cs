

using SGP.Core.Interfaces;
using SGP.Core.Interfaces.IRepositories;
using SGP.Infrastructure.Data;
using SGP.Infrastructure.Repositories;

namespace SGP.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext context)
        {  
            ItemCategories = new ItemCategoryRepository(context);
        }


        public IItemCategoryRepository ItemCategories { get; private set; }


        public async Task CompleteAsync()
        {
                await _dbContext.SaveChangesAsync();

        }
    }
}
