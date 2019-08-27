using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService _service;

        public ReportsController(BikesContext context)
        {
            _service = new ReportsService(context);
        }

        [Route("api/[controller]/SalesPersonCommission")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPersonCommission>>> Get([FromQuery] int year,[FromQuery] int quarter)
        {
            return Ok(await _service.GetQuarterlySalesPersonCommissionReport(year, quarter));
        }
    }
}
