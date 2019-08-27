using System;
using System.Collections.Generic;
using System.Text;
using BeSpokedBikes.DAL;
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
        }
    }
}
