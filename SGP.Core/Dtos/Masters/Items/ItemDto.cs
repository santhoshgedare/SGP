using SGP.Core.Entities.Items;
using SGP.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Dtos
{
    public class ItemDto
    {
        [Required, StringLength(150)]
        public string Name { get;  set; } = string.Empty;

        [StringLength(50)]
        public string? Sku { get;  set; } // Unique Stock Keeping Unit

        [Required]
        public long CategoryId { get;  set; }

        [ForeignKey(nameof(CategoryId))]
        public ItemCategory Category { get;  set; } = null!;

        [StringLength(500)]
        public string? Description { get;  set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Base Price (₹)")]
        public decimal BasePrice { get;  set; }

        [Required, Range(0, int.MaxValue)]
        [Display(Name = "Available Quantity")]
        public int Quantity { get;  set; }

        public bool IsActive { get;  set; } = true;
        public bool IsCurrent { get;  set; } = true;
        public int RevisionNumber { get;  set; } = 1;
        public long BaseId { get;  set; }

        public StatusEnum Status { get;  set; }

        // Navigation Collections
        public ICollection<ItemImage> Images { get;  set; } = new List<ItemImage>();
        public ICollection<ItemLot> Lots { get;  set; } = new List<ItemLot>();
        public ICollection<ItemDiscount> Discounts { get;  set; } = new List<ItemDiscount>();

        // Computed
        public bool InStock => Quantity > 0;
    }
}
