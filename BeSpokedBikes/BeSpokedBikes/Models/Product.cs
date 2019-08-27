using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikes.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Style { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; } = 0;

        [Required]
        public decimal SalePrice { get; set; } = 0;

        [Required]
        public int QuantityAvailable { get; set; } = 0;

        [Required] public decimal CommissionPercentage { get; set; } = 0;
    }
}
