using System.Collections.Generic;
using BeSpokedBikes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _service;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer value)
        {
            var created = _service.Insert(value);
            return Created($"", created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer value)
        {
            return Ok(_service.Update(id, value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
