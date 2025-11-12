using SGP.Core.Entities.Items;
using SGP.Core.Interfaces.IRepositories;
using SGP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Infrastructure.Repositories
{
    public class ItemCategoryRepository : RevisionRepository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
