using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set;}
        public Product Product { get; set; }
        [ForeignKey("SalesPerson")]
        public int SalesPersonId { get; set;}
        public SalesPerson SalesPerson { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime SalesDate { get; set; }
    }
}