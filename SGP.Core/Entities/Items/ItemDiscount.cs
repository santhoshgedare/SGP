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
    [Index(nameof(ItemId))]                 // Fast lookups by item
    [Index(nameof(StartDate))]              // Useful for active discount queries
    [Index(nameof(EndDate))]
    [Index(nameof(IsActive))]               // Optional persisted column for quick filtering
    public class ItemDiscount : BaseEntity
    {
        [Required]
        public long ItemId { get; private set; }

        [Range(0, 100)]
        [Display(Name = "Discount (%)")]
        public double DiscountPercent { get; private set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Flat Discount (₹)")] 
        public decimal? FlatAmount { get; private set; }  // Optional fixed-amount discount

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; private set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; private set; }

        [StringLength(200)]
        public string? Description { get; private set; }

        public bool IsPercentage { get; private set; } = true;

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; private set; } = null!;

        // Backing field to allow persisted index on IsActive if needed
        public bool IsActive { get; private set; } = true;

    }

}
