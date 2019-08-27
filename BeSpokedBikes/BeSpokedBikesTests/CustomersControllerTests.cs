using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.Controllers;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeSpokedBikesTests
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private CustomersController _controller;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikes;Trusted_Connection=True;";

        private BikesContext _context;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new BikesContext(builder.Options);
            _controller = new CustomersController(_context);
        }

        [Test]
        public async Task GetAllCustomers()
        {
            ActionResult<IEnumerable<Customer>> response = await _controller.Get();
            var customers = response.Value;
            Assert.IsNotNull(customers);
        }
    }

    [TestFixture]
    public class CustomersServiceTests
    {
        private CustomersService _service;
        private BikesContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikes;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new BikesContext(builder.Options);
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