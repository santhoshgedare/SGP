using SGP.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Interfaces.IRepositories
{
    public interface IActiveRepository<T> : IRepository<T> where T : AggregateRoot, IStatus
    {
        Task<List<T>> ActiveToListAsync();
    }
}
