

using SGP.Core.Entities.Items;
using SGP.Core.Interfaces.IRepositories;

namespace SGP.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IItemCategoryRepository ItemCategories { get; }

        Task CompleteAsync();
    }
}
