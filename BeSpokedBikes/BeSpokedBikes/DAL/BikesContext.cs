﻿using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.DAL
{
    public class BikesContext : DbContext
    {
        public BikesContext()
        {
            Database.EnsureCreated();
        }

        // ReSharper disable once UnusedMember.Global
        public BikesContext(DbContextOptions<BikesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // For Unit Testing
        protected BikesContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
    }
}