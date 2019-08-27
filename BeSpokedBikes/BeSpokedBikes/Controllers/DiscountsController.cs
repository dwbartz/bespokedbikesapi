using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly DiscountsService _service;

        public DiscountsController(BikesContext context)
        {
            this._service = new DiscountsService(context);
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> Get()
        {
            return Ok(await _service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> Get(int id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Discount>> PostAsync([FromBody] Discount value)
        {
            var created = await _service.Insert(value);
            return Created($"", created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Discount>> Put(int id, [FromBody] Discount value)
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
