using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Interfaces
{
    public interface IRevision
    {
        long BaseId { get; }
        int RevisionNumber { get; }
        bool IsCurrent { get; }
    }
}
