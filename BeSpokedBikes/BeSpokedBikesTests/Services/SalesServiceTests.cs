using System;
using System.Threading.Tasks;
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
        private TestContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikes;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new TestContext(builder.Options);
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
                    SalesDate = DateTime.UtcNow
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
        public async Task GetSale()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
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