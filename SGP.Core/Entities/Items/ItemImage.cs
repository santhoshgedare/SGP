using Microsoft.EntityFrameworkCore;
using SGP.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Entities.Items
{
    [Index(nameof(ItemId))]                
    [Index(nameof(IsPrimary))]             
    [Index(nameof(DisplayOrder))]          
    public class ItemImage : BaseEntity
    {
        [Required]
        public long ItemId { get; private set; }

        [Required, StringLength(250)]
        public string ImageUrl { get; private set; } = string.Empty;

        [StringLength(100)]
        public string? AltText { get; private set; }

        public bool IsPrimary { get; private set; } = false;

        public int DisplayOrder { get; private set; } = 0;

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; private set; } = null!;
    }

}
