using System;

namespace BeSpokedBikes.Models
{
    public class SalesPerson
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Phone { get; }
        public DateTime StartDate { get; }
        public DateTime TerminationDate { get; }
        public string Manager { get; }
    }
}
