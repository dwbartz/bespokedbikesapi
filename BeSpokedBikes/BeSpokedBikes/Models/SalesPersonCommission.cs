using System;

namespace BeSpokedBikes.Models
{
    public class SalesPersonCommission
    {
        public SalesPerson SalesPerson { get; set; }
        public decimal Commission { get; set; }
        public DateTime StartDate { get;set; }
        public DateTime EndDate { get;set; }
    }
}
