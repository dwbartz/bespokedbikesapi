using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class CustomersService
    {
        private readonly BikesContext _context;

        public CustomersService(BikesContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FirstAsync(x => x.Id == id);
        }

        public async Task<Customer> Insert(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Update(int id, Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}