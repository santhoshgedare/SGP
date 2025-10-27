using SGP.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Interfaces.IRepositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T> SingleAsync(long id);
        Task<T> FindAsync(long id);
        Task<T> SingleAsync(string uid);
        Task<List<T>> ToListAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
