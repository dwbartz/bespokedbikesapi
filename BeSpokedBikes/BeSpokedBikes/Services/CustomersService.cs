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
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return await GetById(customer.Id);
        }

        public async Task<Customer> Update(Customer value)
        {
            var customer = await GetById(value.Id);

            if (customer == null)
            {
                throw new ArgumentException($"Cannot update {nameof(Customer)} for Id {value.Id}, {nameof(Customer)} not found");
            }

            customer.Address = value.Address;
            customer.FirstName = value.FirstName;
            customer.LastName = value.LastName;
            customer.Phone = value.Phone;
            customer.StartDate = value.StartDate;

            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }
        
        public async Task Remove(int id)
        {
            var customer = await GetById(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}