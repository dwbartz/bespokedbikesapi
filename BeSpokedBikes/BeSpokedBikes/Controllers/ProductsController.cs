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
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _service;

        public ProductsController(BikesContext context)
        {
            _service = new ProductsService(context);
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _service.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product value)
        {
            var sale = await _service.Insert(value);
            return Created(Url.Action("Get", sale.Id), sale);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product value)
        {
            if (id != value.Id)
            {
                throw new ArgumentException("IDs do not match");
            }

            return Ok(await _service.Update(id, value));
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
