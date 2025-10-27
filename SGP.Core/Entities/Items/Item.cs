using Microsoft.EntityFrameworkCore;
using SGP.Core.Enums;
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
    [Index(nameof(Name))]  
    [Index(nameof(CategoryId))] 
    [Index(nameof(Sku), IsUnique = true)]  
    [Index(nameof(IsActive))] 
    [Index(nameof(IsCurrent))]
    [Index(nameof(BaseId), nameof(IsCurrent))]
    public class Item : AggregateRoot
    {
        [Required, StringLength(150)]
        public string Name { get; private set; } = string.Empty;

        [StringLength(50)]
        public string? Sku { get; private set; } // Unique Stock Keeping Unit

        [Required]
        public long CategoryId { get; private set; }

        [ForeignKey(nameof(CategoryId))]
        public ItemCategory Category { get; private set; } = null!;

        [StringLength(500)]
        public string? Description { get; private set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Base Price (₹)")]
        public decimal BasePrice { get; private set; }

        [Required, Range(0, int.MaxValue)]
        [Display(Name = "Available Quantity")]
        public int Quantity { get; private set; }

        public bool IsActive { get; private set; } = true;
        public bool IsCurrent { get; private set; } = true;
        public int RevisionNumber { get; private set; } = 1;
        public long BaseId { get; private set; }

        public StatusEnum Status { get; private set; }

        // Navigation Collections
        public ICollection<ItemImage> Images { get; private set; } = new List<ItemImage>();
        public ICollection<ItemLot> Lots { get; private set; } = new List<ItemLot>();
        public ICollection<ItemDiscount> Discounts { get; private set; } = new List<ItemDiscount>();

        // Computed
        public bool InStock => Quantity > 0;
    }

}
