using System;

namespace BeSpokedBikes.Models
{
    public class Discount
    {
        public int ProductId { get; }
        public DateTime BeginDate { get; }
        public DateTime EndDate { get; }
        public decimal DiscountPercentage { get; }
    }
}