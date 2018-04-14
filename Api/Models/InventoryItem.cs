namespace Api.Models
{
    public class InventoryItem
    {
        public string InventoryItemId { get; set; }
        public int OrderNumber { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }  
    }
}