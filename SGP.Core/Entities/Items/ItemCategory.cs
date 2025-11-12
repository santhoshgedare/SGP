using Microsoft.EntityFrameworkCore;
using SGP.Core.Enums;
using SGP.Core.Interfaces;
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
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Status))]
    [Index(nameof(IsCurrent))]
    [Index(nameof(BaseId), nameof(IsCurrent))]
    [Index(nameof(ParentCategoryId))]
    public class ItemCategory : AggregateRoot, IRevision
    {
        [Required, StringLength(100)]
        public string Name { get; private set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; private set; }
        public bool IsCurrent { get; private set; } = true;
        public int RevisionNumber { get; private set; } = 0;
        public long BaseId { get; private set; }

        public StatusEnum Status { get; private set; } = StatusEnum.Active;

        // 🧩 Hierarchy (Self Reference)
        public long? ParentCategoryId { get; private set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public ItemCategory? ParentCategory { get; private set; }

        public ICollection<ItemCategory> SubCategories { get; private set; } = new List<ItemCategory>();

        // 🔗 Relationship with Items
        public ICollection<Item> Items { get; private set; } = new List<Item>();
        private ItemCategory() { }

        public ItemCategory(string name, string? description, StatusEnum status, long? parentCategoryId)
        {
            Name = name;
            Description = description;
            Status = status;
            ParentCategoryId = parentCategoryId;
            IsCurrent = true;
            RevisionNumber = RevisionNumber + 1;
        }

        public void SetBaseId(long baseId)
        {
            BaseId = baseId;
        }


    }
}
