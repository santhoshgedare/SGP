

using SGP.Core.Interfaces;
using SGP.Infrastructure.Data;

namespace SGP.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
       

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
            

        }

        public async Task CompleteAsync()
        {
                await _dbContext.SaveChangesAsync();

        }
    }
}
