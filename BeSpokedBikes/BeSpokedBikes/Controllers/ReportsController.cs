using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService _service;

        public ReportsController(BikesContext context)
        {
            _service = new ReportsService(context);
        }
        
        [HttpGet]
        [Route("SalesPersonCommission")]
        public async Task<ActionResult<IEnumerable<SalesPersonCommission>>> Get([FromQuery] int year,[FromQuery] int quarter)
        {
            return Ok(await _service.GetQuarterlySalesPersonCommissionReport(year, quarter));
        }
    }
}
