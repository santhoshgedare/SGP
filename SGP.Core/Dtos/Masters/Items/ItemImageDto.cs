using SGP.Core.Entities.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Dtos.Masters.Items
{
    public class ItemImageDto
    {
        [Required]
        public long ItemId { get;  set; }

        [Required, StringLength(250)]
        public string ImageUrl { get;  set; } = string.Empty;

        [StringLength(100)]
        public string? AltText { get;  set; }

        public bool IsPrimary { get;  set; } = false;

        public int DisplayOrder { get;  set; } = 0;

        [ForeignKey(nameof(ItemId))]
        public Item Item { get;  set; } = null!;
    }
}
