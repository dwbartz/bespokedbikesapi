using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set;}
        public Product Product { get; set; }

        [Required]
        [ForeignKey("SalesPerson")]
        public int SalesPersonId { get; set;}
        public SalesPerson SalesPerson { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime SalesDate { get; set; }
    }
}