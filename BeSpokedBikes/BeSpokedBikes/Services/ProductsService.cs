using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class ProductsService
    {
        private readonly BikesContext _context;

        public ProductsService(BikesContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstAsync(x => x.Id == id);
        }

        public async Task<Product> Insert(Product value)
        {
            if (await _context.Products.AnyAsync(x =>
                x.Id == value.Id || (x.Name == value.Name && x.Manufacturer == value.Manufacturer)))
            {
                throw new ArgumentException("Product already exists!");
            }

            _context.Products.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<Product> Update(int id, Product value)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var product = await GetById(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}