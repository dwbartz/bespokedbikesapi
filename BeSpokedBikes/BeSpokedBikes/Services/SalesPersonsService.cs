using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class SalesPersonsService
    {
        private readonly BikesContext _context;

        public SalesPersonsService(BikesContext context)
        {
            _context = context;
        }

        public async Task<IList<SalesPerson>> GetAll()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<SalesPerson> GetById(int id)
        {
            return await _context.SalesPersons.FirstAsync(x => x.Id == id);
        }

        public async Task<SalesPerson> Insert(SalesPerson value)
        {
            if (await _context.SalesPersons.AnyAsync(x =>
                x.Id == value.Id || (x.FirstName == value.FirstName && x.LastName == value.LastName)))
            {
                throw new ArgumentException("SalesPerson already exists!");
            }

            _context.SalesPersons.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<SalesPerson> Update(int id, SalesPerson value)
        {
            throw new NotImplementedException();
        }
        
        public async Task Remove(int id)
        {
            var salesPerson = await GetById(id);

            if (salesPerson != null)
            {
                _context.SalesPersons.Remove(salesPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}