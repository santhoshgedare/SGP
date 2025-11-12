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
    public class ItemCategoryDto : AggregateRootDto
    {
        [Required, StringLength(100)]
        public string Name { get;  set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get;  set; }
        public bool IsCurrent { get;  set; } = true;
        public int RevisionNumber { get;  set; } = 1;
        public long BaseId { get;  set; }

        public StatusEnum Status { get;  set; } = StatusEnum.Active;

        // 🧩 Hierarchy (Self Reference)
        public long? ParentCategoryId { get;  set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public ItemCategory? ParentCategory { get;  set; }

        public ICollection<ItemCategory> SubCategories { get;  set; } = new List<ItemCategory>();

        // 🔗 Relationship with Items
        public ICollection<Item> Items { get;  set; } = new List<Item>();
       
    }
}
