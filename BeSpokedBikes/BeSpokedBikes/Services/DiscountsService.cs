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
            throw new NotImplementedException();
        }

        public async Task<Discount> Update(int id, Discount value)
        {
            throw new NotImplementedException();
        }
        
        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}