using SGP.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Dtos
{
    public class AggregateRootDto : BaseEntityDto
    { 
        public string? Uid { get;  set; }
        public long CreatedBy { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public long? UpdatedBy { get;  set; }
        public DateTime? UpdatedDate { get;  set; }

       
    }
}
