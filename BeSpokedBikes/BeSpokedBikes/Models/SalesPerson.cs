using System;
using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikes.Models
{
    public class SalesPerson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime TerminationDate { get; set; }
        [Required]
        public string Manager { get; set; }
    }
}
