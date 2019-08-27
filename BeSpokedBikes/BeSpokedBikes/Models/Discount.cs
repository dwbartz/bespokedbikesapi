using System;

namespace BeSpokedBikes.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}