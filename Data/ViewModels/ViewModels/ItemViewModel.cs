namespace ViewModels
{
    public class ItemViewModel
    {
        public string InventoryItemId { get; set; }
        public int OrderNumber { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public CategoryViewModel Category { get; set; }
        public string CategoryId { get; set; }
    }
}
