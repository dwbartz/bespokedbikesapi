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
    public class DiscountsServiceTests
    {
        private DiscountsService _service;
        private BikesContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikesTests;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BikesContext>().UseSqlServer(ConnectionString);
            _context = new BikesContext(builder.Options);
            _service = new DiscountsService(_context);

            using (var transaction = _context.Database.BeginTransaction())
            {
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
                
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Products] OFF");

                _context.Discounts.Add(new Discount
                {
                    Id = 1,
                    ProductId = 1,
                    BeginDate = DateTime.UtcNow,
                    DiscountPercentage = new decimal(.20),
                    EndDate = DateTime.UtcNow.AddDays(2)
                });
                
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Discounts] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Discounts] OFF");
                transaction.Commit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.Discounts);
            _context.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllDiscounts()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetDiscount()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
        }
        
        [Test]
        public async Task GetAllDiscounts_Details()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result.First().Product);
        }

        [Test]
        public async Task GetDiscount_Details()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result.Product);
        }

        [Test]
        public async Task AddDiscount()
        {
            var result = await _service.Insert(new Discount
            {
                BeginDate = DateTime.UtcNow,
                DiscountPercentage = new decimal(.30),
                EndDate = DateTime.UtcNow.AddDays(2),
                ProductId = 1
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public async Task UpdateDiscount()
        {
            var result = await _service.Update(new Discount
            {
                Id = 1,
                ProductId = 1,
                BeginDate = DateTime.UtcNow,
                DiscountPercentage = new decimal(.25),
                EndDate = DateTime.UtcNow.AddDays(2)
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void RemoveDiscount()
        {
            Assert.DoesNotThrowAsync(async () => await _service.Remove(1));
        }
    }
}