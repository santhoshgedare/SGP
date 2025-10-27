 
using Microsoft.EntityFrameworkCore;
using SGP.Core.Interfaces.IRepositories;
using SGP.Core.SharedKernel;
using SGP.Infrastructure.Data;

namespace SGP.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> SingleAsync(long id)
        {
            return await _dbContext.Set<T>().SingleAsync(e => e.Id == id);
        }

        public async Task<T> FindAsync(long id)
        { 
            return await _dbContext.Set<T>().FindAsync(id); 
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<T> SingleAsync(string uid)
        {
            return await _dbContext.Set<T>().SingleAsync(e => e.Uid == uid);
        }

    }
}
