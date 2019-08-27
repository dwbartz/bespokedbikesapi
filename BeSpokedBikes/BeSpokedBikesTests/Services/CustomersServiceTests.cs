using System;
using System.Threading.Tasks;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeSpokedBikesTests.Services
{
    [TestFixture]
    public class CustomersServiceTests
    {
        private CustomersService _service;
        private TestContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikesTests;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new TestContext(builder.Options);
            _service = new CustomersService(_context);

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Customers.Add(new Customer
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    Address = "1 Street",
                    Phone = "123-123-1234",
                    StartDate = DateTime.UtcNow
                });

                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [Customers] OFF");
                transaction.Commit();
            }

        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.Customers);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllCustomers()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetCustomer()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddCustomer()
        {
            var result = await _service.Insert(new Customer
            {
                FirstName = "First",
                LastName = "Last",
                Address = "1 Street",
                Phone = "123-123-1234",
                StartDate = DateTime.UtcNow
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public async Task UpdateCustomer()
        {
            var result = await _service.Update(new Customer
            {
                Id = 1,
                FirstName = "First",
                LastName = "Last",
                Address = "1 Street",
                Phone = "123-123-1234",
                StartDate = DateTime.UtcNow
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void RemoveCustomer()
        {
            Assert.DoesNotThrowAsync(async () => await _service.Remove(1));
        }
    }
}