using System.Collections.Generic;

namespace Entities.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public virtual List<InventoryItem> Items { get; set; }
    }
}
