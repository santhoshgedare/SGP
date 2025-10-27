 
using Microsoft.EntityFrameworkCore;
using SGP.Core.Interfaces;
using SGP.Core.Interfaces.IRepositories;
using SGP.Core.SharedKernel;
using SGP.Infrastructure.Data;
using SGP.Infrastructure.Repositories;

namespace SGP.Infrastructure.Repositories
{
    public class ActiveRepository<T> : Repository<T>, IActiveRepository<T> where T : AggregateRoot, IStatus
    {
        public ActiveRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<T>> ActiveToListAsync()
        {
            return await _dbContext.Set<T>().Where(s => s.Status == Core.Enums.StatusEnum.Active).ToListAsync();
        }

    }
}
