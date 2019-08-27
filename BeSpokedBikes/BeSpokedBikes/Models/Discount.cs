using System;
using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikes.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal DiscountPercentage { get; set; }
    }
}