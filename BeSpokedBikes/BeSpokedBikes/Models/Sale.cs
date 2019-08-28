using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        // Todo: Should we compute the price paid by the customer and store it here historically?

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set;}
        public Product Product { get; set; }

        [Required]
        [ForeignKey(nameof(SalesPerson))]
        public int SalesPersonId { get; set;}
        public SalesPerson SalesPerson { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime SalesDate { get; set; }
    }
}