using System;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public Customer Customer { get; set; }
        public DateTime SalesDate { get; set; }
    }

    public class Sale2
    {
        public int ProductId { get; }
        public int SalesPersonId { get; }
        public int CustomerId { get; }
        public DateTime SalesDate { get; }
    }
}