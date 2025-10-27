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
    [Index(nameof(MinQuantity))]                 
    [Index(nameof(LotRate))]                   
    public class ItemLot : BaseEntity
    {
        [Required]
        public long ItemId { get; private set; }

        [Range(1, int.MaxValue)]
        public int MinQuantity { get; private set; }

        [Range(0, double.MaxValue)]
        public decimal LotRate { get; private set; }

        [StringLength(100)]
        public string? Description { get; private set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; private set; } = null!;
    }

}
