using System;
using System.Threading.Tasks;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeSpokedBikesTests.Services
{
    [TestFixture]
    public class SalesPersonsServiceTests
    {
        private SalesPersonsService _service;
        private TestContext _context;

        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BeSpokedBikesTests;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            _context = new TestContext(builder.Options);
            _service = new SalesPersonsService(_context);

            using (var transaction = _context.Database.BeginTransaction())
            {
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
                
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [SalesPersons] OFF");
                transaction.Commit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.RemoveRange(_context.SalesPersons);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllSalesPersons()
        {
            var result = await _service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetSalesPerson()
        {
            var result = await _service.GetById(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddSalesPerson_fails_duplicate()
        {
            Assert.ThrowsAsync<ArgumentException>(async() => await _service.Insert(new SalesPerson
            {
                FirstName = "Harecore",
                LastName = "Salesman",
                Address = "1 Street Ln",
                Phone = "321-321-4321",
                TerminationDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                Manager = "Joe"
            }));
        }
        
        [Test]
        public async Task AddSalesPerson()
        {
            var result = await _service.Insert(new SalesPerson
            {
                FirstName = "Mediocre",
                LastName = "Sales Guy",
                Address = "2 Street Ln",
                Phone = "321-321-4321",
                TerminationDate = DateTime.MaxValue,
                StartDate = DateTime.MinValue,
                Manager = "Joe"
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public async Task UpdateSalesPerson()
        {
            var result = await _service.Update(new SalesPerson
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

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void RemoveSalesPerson()
        {
            Assert.DoesNotThrowAsync(async () => await _service.Remove(1));
        }
    }
}