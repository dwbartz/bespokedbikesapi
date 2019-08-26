using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpokedBikes.Models
{
    public class Product
    {
        public string Name { get; }
        public string Manufacturer { get; }
        public string Style { get; }
        public decimal PurchasePrice { get; }
        public decimal SalePrice { get; }
        public string QuantityAvailable { get; }
        public decimal CommissionPercentage { get; }
    }
}
