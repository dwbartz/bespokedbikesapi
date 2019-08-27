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

        public Product Insert(Product value)
        {
            throw new NotImplementedException();
        }

        public Product Update(int id, Product value)
        {
            throw new NotImplementedException();
        }
    }
}