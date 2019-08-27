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

        public SalesPerson Insert(SalesPerson value)
        {
            throw new NotImplementedException();
        }

        public SalesPerson Update(int id, SalesPerson value)
        {
            throw new NotImplementedException();
        }
    }
}