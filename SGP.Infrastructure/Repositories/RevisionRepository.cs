using Microsoft.EntityFrameworkCore;
using SGP.Core.Interfaces;
using SGP.Core.Interfaces.IRepositories;
using SGP.Core.SharedKernel;
using SGP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Infrastructure.Repositories
{
    public class RevisionRepository<T> : Repository<T>, IRevisionRepository<T> where T : AggregateRoot, IRevision
    {
        public RevisionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<T>> GetRevisions(long baseId)
        {
            return await _dbContext.Set<T>().Where(s => s.BaseId == baseId).ToListAsync();
        }
        public async Task<List<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().Where(s => s.IsCurrent == true).OrderByDescending(s => s.CreatedDate).ToListAsync();
        }
    }
}
