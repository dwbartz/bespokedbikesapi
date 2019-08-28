using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    [ApiController]
    public class SalesPersonsController : ControllerBase
    {
        private readonly SalesPersonsService _service;

        public SalesPersonsController(SalesPersonsService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> Get()
        {
            return Ok(await _service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> Get(int id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalesPerson value)
        {
            var salesPerson = await _service.Insert(value);
            return Created(Url.Action("Get", salesPerson.Id), salesPerson);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalesPerson value)
        {
            if (id != value.Id)
            {
                throw new ArgumentException("IDs do not match");
            }

            return Ok(await _service.Update(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Remove(id);
            return Ok();
        }
    }
}
