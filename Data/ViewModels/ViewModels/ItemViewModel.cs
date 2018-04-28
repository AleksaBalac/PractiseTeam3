using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ItemViewModel
    {
        public string InventoryItemId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        public string BarCode { get; set; }
        public CategoryViewModel Category { get; set; }
        public string CategoryId { get; set; }
    }
}
