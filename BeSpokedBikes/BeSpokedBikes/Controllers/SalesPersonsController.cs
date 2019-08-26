using System.Collections.Generic;
using BeSpokedBikes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonsController : ControllerBase
    {
        private readonly SalesPersonsService _service;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SalesPerson>> Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<SalesPerson> Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] SalesPerson value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SalesPerson value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
