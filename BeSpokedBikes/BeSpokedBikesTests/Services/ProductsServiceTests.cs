using System;
using System.Threading.Tasks;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeSpokedBikesTests.Services
{
    [TestFixture]
    public class ProductsServiceTests
    {
        private ProductsService _service;
        private TestContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikes;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new TestContext(builder.Options);
            _service = new ProductsService(_context);

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
                transaction.Commit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.Products);
            _context.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllProducts()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetProduct()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddProduct()
        {
            var result = await _service.Insert(new Product
            {
                Manufacturer = "Acme's Competitor",
                Name = "Toy That's Really Similar to Acme's",
                SalePrice = new decimal(1.25),
                Style = "Toddler",
                PurchasePrice = new decimal(1.50),
                QuantityAvailable = 5,
                CommissionPercentage = new decimal(.05)
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }
        
        [Test]
        public void AddProduct_Fails_DuplicateProduct()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Insert(new Product
            {
                Manufacturer = "Acme",
                Name = "Toy",
                SalePrice = new decimal(1.25),
                Style = "Toddler",
                PurchasePrice = new decimal(1.50),
                QuantityAvailable = 5,
                CommissionPercentage = new decimal(.05)
            }));
        }

        [Test]
        public async Task UpdateProduct()
        {
            var result = await _service.Update(new Product
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

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void RemoveProduct()
        {
            Assert.DoesNotThrowAsync(async () => await _service.Remove(1));
        }
    }
}