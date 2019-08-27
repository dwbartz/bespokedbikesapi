namespace BeSpokedBikes.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Style { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string QuantityAvailable { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}
