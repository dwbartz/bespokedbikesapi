using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; }
        public Product Product { get; set; }
        [ForeignKey("SalesPerson")]
        public int SalesPersonId { get; }
        public SalesPerson SalesPerson { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; }
        public Customer Customer { get; set; }
        public DateTime SalesDate { get; set; }
    }
}