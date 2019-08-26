using System;

namespace BeSpokedBikes.Models
{
    public class Sale
    {
        public Product Product { get; }
        public SalesPerson SalesPerson { get; }
        public Customer Customer { get; }
        public DateTime SalesDate { get; }
    }
}