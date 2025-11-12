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
    public class ItemLotDto
    {
        [Required]
        public long ItemId { get;  set; }

        [Range(1, int.MaxValue)]
        public int MinQuantity { get;  set; }

        [Range(0, double.MaxValue)]
        public decimal LotRate { get;  set; }

        [StringLength(100)]
        public string? Description { get;  set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get;  set; } = null!;
    }
}
