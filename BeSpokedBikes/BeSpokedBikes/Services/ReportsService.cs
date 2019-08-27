using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class ReportsService
    {
        private readonly BikesContext _context;

        public ReportsService(BikesContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public async Task<IList<SalesPersonCommission>> GetQuarterlySalesPersonCommissionReport(int year, int quarter)
        {
            DateTime startDate;
            DateTime endDate;

            switch (quarter)
            {
                case 1:
                    startDate = DateTime.Parse($"1-1-{year}");
                    endDate = DateTime.Parse($"3-31-{year}");
                    break;
                case 2:
                    startDate = DateTime.Parse($"4-1-{year}");
                    endDate = DateTime.Parse($"6-30-{year}");
                    break;
                case 3:
                    startDate = DateTime.Parse($"7-1-{year}");
                    endDate = DateTime.Parse($"9-30-{year}");
                    break;
                case 4:
                    startDate = DateTime.Parse($"10-1-{year}");
                    endDate = DateTime.Parse($"12-31-{year}");
                    break;
                default:
                    throw new ArgumentException();
            }

            var salesDuringQuarter = _context.Sales.Where(x => x.SalesDate >= startDate && x.SalesDate <= endDate);
            var salesPersons = salesDuringQuarter.Select(x => x.SalesPerson).Distinct();

            // This may work better in the future as a Window function in pure SQL
            var report = salesPersons
                .GroupJoin(salesDuringQuarter, x => x.Id, x => x.SalesPersonId, (salesPerson, sales) =>
                    new SalesPersonCommission
                    {
                        SalesPerson = salesPerson,
                        Commission = sales.Sum(x => x.Product.CommissionPercentage * x.Product.SalePrice),
                        StartDate = startDate,
                        EndDate = endDate
                    });

            return await report.ToListAsync();
        }
    }
}