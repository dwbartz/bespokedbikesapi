﻿using System;
using System.Linq;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeSpokedBikesTests.Services
{
    [TestFixture]
    public class SalesServiceTests
    {
        private SalesService _service;
        private BikesContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikesTests;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BikesContext>().UseSqlServer(ConnectionString);
            _context = new BikesContext(builder.Options);
            _service = new SalesService(_context);

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] ON");
                _context.Customers.Add(new Customer
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    Address = "1 Street",
                    Phone = "123-123-1234",
                    StartDate = DateTime.UtcNow
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] OFF");

                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] ON");
                _context.Products.Add(new Product
                {
                    Id = 1,
                    Manufacturer = "Acme",
                    Name = "Toy",
                    SalePrice = new decimal(1.25),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.05)
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] OFF");

                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] ON");
                _context.SalesPersons.Add(new SalesPerson
                {
                    Id = 1,
                    FirstName = "Harecore",
                    LastName = "Salesman",
                    Address = "1 Street Ln",
                    Phone = "321-321-4321",
                    TerminationDate = DateTime.MaxValue,
                    StartDate = DateTime.MinValue,
                    Manager = "Joe"
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] OFF");

                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Sales] ON");
                _context.Sales.Add(new Sale
                {
                    Id = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 1,
                    SalesDate = DateTime.Today
                });
                _context.Sales.Add(new Sale
                {
                    Id = 2,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 1,
                    SalesDate = DateTime.Today
                });
                _context.Sales.Add(new Sale
                {
                    Id = 3,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 1,
                    SalesDate = DateTime.UtcNow.AddDays(1)
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Sales] OFF");
                transaction.Commit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.Customers);
            _context.RemoveRange(_context.Products);
            _context.RemoveRange(_context.Sales);
            _context.RemoveRange(_context.SalesPersons);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllSales()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task GetAllSales_HasDetails()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result.First().Product);
            Assert.IsNotNull(result.First().Customer);
            Assert.IsNotNull(result.First().SalesPerson);
        }
        
        [Test]
        public async Task GetAllSales_FilterByStartDate()
        {
            var result = await _service.GetAll(startDate: DateTime.Today.AddDays(1));
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public async Task GetAllSales_FilterByEndDate()
        {
            var result = await _service.GetAll(endDate: DateTime.Today);
            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public async Task GetAllSales_NoFilter()
        {
            var result = await _service.GetAll();
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task GetSale()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task GetSale_HasDetails()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result.Product);
            Assert.IsNotNull(result.Customer);
            Assert.IsNotNull(result.SalesPerson);
        }

        [Test]
        public async Task AddSale()
        {
            var result = await _service.Insert(new Sale
            {
                CustomerId = 1,
                ProductId = 1,
                SalesPersonId = 1,
                SalesDate = DateTime.UtcNow
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public async Task UpdateSale()
        {
            var result = await _service.Update(new Sale
            {
                Id = 1,
                CustomerId = 1,
                ProductId = 1,
                SalesPersonId = 1,
                SalesDate = DateTime.UtcNow
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void RemoveSale()
        {
            Assert.DoesNotThrowAsync(async () => await _service.Remove(1));
        }
    }
}