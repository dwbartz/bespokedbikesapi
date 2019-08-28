using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class SalesService
    {
        private readonly BikesContext _context;

        public SalesService(BikesContext context)
        {
            _context = context;
        }

        public async Task<IList<Sale>> GetAll(DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Sale> sales = _context.Sales;

            if (startDate.HasValue)
            {
                sales = sales.Where(x => x.SalesDate >= startDate.Value);
            }
            
            if (endDate.HasValue)
            {
                sales = sales.Where(x => x.SalesDate <= endDate.Value);
            }

            return await sales
                .Include(x => x.Product)
                .Include(x => x.SalesPerson)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<Sale> GetById(int id)
        {
            return await _context.Sales
                .Include(x => x.Product)
                .Include(x => x.SalesPerson)
                .Include(x => x.Customer)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Sale> Insert(Sale value)
        {
            _context.Sales.Add(value);
            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }

        public async Task<Sale> Update(Sale value)
        {
            var sale = await GetById(value.Id);

            if (sale == null)
            {
                throw new ArgumentException($"Cannot update {nameof(Sale)} for Id {value.Id}, {nameof(Sale)} not found");
            }

            sale.CustomerId = value.CustomerId;
            sale.ProductId = value.ProductId;
            sale.SalesPersonId = value.SalesPersonId;
            
            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }
        
        public async Task Remove(int id)
        {
            var sale = await GetById(id);

            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}