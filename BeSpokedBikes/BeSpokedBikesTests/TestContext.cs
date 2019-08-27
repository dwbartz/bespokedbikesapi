using BeSpokedBikes.DAL;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikesTests
{
    public class TestContext : BikesContext
    {
        public TestContext(DbContextOptions options) : base(options)
        {
        }
    }
}
