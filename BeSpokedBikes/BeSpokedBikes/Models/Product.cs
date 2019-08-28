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

        // TODO: Is this the cost to the supplier or to the customer?
        [Required]
        public decimal PurchasePrice { get; set; } = 0;

        // TODO: Is this supposed to computed with the discount?
        [Required]
        public decimal SalePrice { get; set; } = 0;

        [Required]
        public int QuantityAvailable { get; set; } = 0;

        [Required] 
        public decimal CommissionPercentage { get; set; } = 0;
    }
}
