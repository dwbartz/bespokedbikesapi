using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class DiscountsService
    {
        private readonly BikesContext _context;

        public DiscountsService(BikesContext context)
        {
            _context = context;
        }

        public async Task<IList<Discount>> GetAll()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount> GetById(int id)
        {
            return await _context.Discounts.FirstAsync(x => x.Id == id);
        }

        public async Task<Discount> Insert(Discount value)
        {
            _context.Discounts.Add(value);
            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }

        public async Task<Discount> Update(Discount value)
        {
            var discount = await GetById(value.Id);

            if (discount == null)
            {
                throw new ArgumentException($"Cannot update {nameof(Discount)} for Id {value.Id}, {nameof(Discount)} not found");
            }

            discount.BeginDate = value.BeginDate;
            discount.DiscountPercentage = value.DiscountPercentage;
            discount.EndDate = value.EndDate;
            discount.ProductId = value.ProductId;

            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }
        
        public async Task Remove(int id)
        {
            var discount = await GetById(id);

            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();
            }
        }
    }
}