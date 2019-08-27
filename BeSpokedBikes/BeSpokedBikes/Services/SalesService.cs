using System;
using System.Collections.Generic;
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

        public async Task<IList<Sale>> GetAll()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetById(int id)
        {
            return await _context.Sales.FirstAsync(x => x.Id == id);
        }

        public async Task<Sale> Insert(Sale value)
        {
            _context.Sales.Add(value);
            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }

        public async Task<Sale> Update(int id, Sale value)
        {
            throw new NotImplementedException();
        }
        
        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}