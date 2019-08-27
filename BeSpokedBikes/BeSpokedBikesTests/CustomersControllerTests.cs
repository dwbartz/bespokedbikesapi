using System.Threading.Tasks;
using BeSpokedBikes.Controllers;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Services;
using NUnit.Framework;

namespace BeSpokedBikesTests
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CustomersController(new BikesContext());
        }

        [Test]
        public async Task GetAllCustomers()
        {
            var result = await _controller.Get();
            var customers = result.Value;
        }
    }

    [TestFixture]
    public class CustomersServiceTests
    {
        private CustomersService service;

        [SetUp]
        public void Setup()
        {
            service = new CustomersService(new BikesContext());
        }

        [Test]
        public async Task GetAllCustomers()
        {
            var result = await service.GetAll();
            Assert.IsNotNull(result);
        }
    }
}
