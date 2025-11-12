using SGP.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Interfaces.IRepositories
{
    public interface IRevisionRepository<T> : IRepository<T> where T : AggregateRoot, IRevision
    {
        Task<List<T>> GetRevisions(long baseId);
        Task<List<T>> ToListAsync();
    }
}
