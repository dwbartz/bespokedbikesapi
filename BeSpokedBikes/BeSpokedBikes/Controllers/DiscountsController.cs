using System.Collections.Generic;
using BeSpokedBikes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly DiscountsService _service;

        public DiscountsController(DiscountsService service)
        {
            this._service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Discount>> Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Discount> Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Discount> Post([FromBody] Discount value)
        {
            var created = _service.Insert(value);
            return Created($"", created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Discount> Put(int id, [FromBody] Discount value)
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
