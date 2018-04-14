using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
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
