using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.Controllers;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
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
            var builder = new DbContextOptionsBuilder<BikesContext>().UseSqlServer(ConnectionString);
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
}