using System;
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
    public class ReportsServiceTests
    {
        private ReportsService _service;
        private BikesContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikesTests;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BikesContext>().UseSqlServer(ConnectionString);
            _context = new BikesContext(builder.Options);
            _service = new ReportsService(_context);

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
                _context.Customers.Add(new Customer
                {
                    Id = 2,
                    FirstName = "First2",
                    LastName = "Last2",
                    Address = "2 Street",
                    Phone = "123-123-9999",
                    StartDate = DateTime.UtcNow
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] OFF");

                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] ON");
                // Commission = .125
                _context.Products.Add(new Product
                {
                    Id = 1,
                    Manufacturer = "Acme",
                    Name = "Toy",
                    SalePrice = new decimal(1.25),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.10)
                });
                // Commission .2625
                _context.Products.Add(new Product
                {
                    Id = 2,
                    Manufacturer = "Acme",
                    Name = "Toy 2",
                    SalePrice = new decimal(1.75),
                    Style = "Toddler",
                    PurchasePrice = new decimal(1.50),
                    QuantityAvailable = 5,
                    CommissionPercentage = new decimal(.15)
                });
                // Commission .05
                _context.Products.Add(new Product
                {
                    Id = 3,
                    Manufacturer = "Acme",
                    Name = "Toy 3",
                    SalePrice = new decimal(1.00),
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
                    FirstName = "Good",
                    LastName = "Salesman",
                    Address = "1 Street Ln",
                    Phone = "321-321-4321",
                    TerminationDate = DateTime.MaxValue,
                    StartDate = DateTime.MinValue,
                    Manager = "Joe"
                });
                _context.SalesPersons.Add(new SalesPerson
                {
                    Id = 2,
                    FirstName = "Better",
                    LastName = "Salesman",
                    Address = "2 Street Ln",
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
                    SalesDate = DateTime.Parse("8/27/19")
                });
                _context.Sales.Add(new Sale
                {
                    Id = 2,
                    CustomerId = 1,
                    ProductId = 1,
                    SalesPersonId = 2,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                _context.Sales.Add(new Sale
                {
                    Id = 3,
                    CustomerId = 2,
                    ProductId = 2,
                    SalesPersonId = 1,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                _context.Sales.Add(new Sale
                {
                    Id = 4,
                    CustomerId = 2,
                    ProductId = 3,
                    SalesPersonId = 2,
                    SalesDate = DateTime.Parse("8/27/19")
                });
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Sales] OFF");
                transaction.Commit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.Discounts);
            _context.RemoveRange(_context.Sales);
            _context.RemoveRange(_context.Customers);
            _context.RemoveRange(_context.Products);
            _context.RemoveRange(_context.SalesPersons);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetForFirstQuarter()
        {
            var report = await _service.GetQuarterlySalesPersonCommissionReport(2019, 3);
            Assert.AreEqual(1, report.First().SalesPerson.Id);
            Assert.AreEqual(2, report.Last().SalesPerson.Id);
            Assert.AreEqual(.3875, report.First().Commission);  
            Assert.AreEqual(.175, report.Last().Commission);
        }

        //TODO: Should the discount be included when computing commission?
    }
}
