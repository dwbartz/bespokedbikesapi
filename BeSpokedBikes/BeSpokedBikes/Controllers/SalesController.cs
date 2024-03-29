﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using BeSpokedBikes.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikes.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _service;

        public SalesController(BikesContext context)
        {
            _service = new SalesService(context);
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> Get([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            return Ok(await _service.GetAll(startDate, endDate));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> Get(int id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Sale value)
        {
            var sale = await _service.Insert(value);
            return Created(Url.Action("Get", sale.Id), sale);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Sale value)
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
