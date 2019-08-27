using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSpokedBikes.DAL;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Services
{
    public class SalesPersonsService
    {
        private readonly BikesContext _context;

        public SalesPersonsService(BikesContext context)
        {
            _context = context;
        }

        public async Task<IList<SalesPerson>> GetAll()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<SalesPerson> GetById(int id)
        {
            return await _context.SalesPersons.FirstAsync(x => x.Id == id);
        }

        public async Task<SalesPerson> Insert(SalesPerson value)
        {
            if (await _context.SalesPersons.AnyAsync(x =>
                x.Id == value.Id || (x.FirstName == value.FirstName && x.LastName == value.LastName)))
            {
                throw new ArgumentException("SalesPerson already exists!");
            }

            _context.SalesPersons.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<SalesPerson> Update(SalesPerson value)
        {
            var salesPerson = await GetById(value.Id);

            if (salesPerson == null)
            {
                throw new ArgumentException($"Cannot update {nameof(SalesPerson)} for Id {value.Id}, {nameof(SalesPerson)} not found");
            }

            salesPerson.FirstName = value.FirstName;
            salesPerson.LastName = value.LastName;
            salesPerson.Address = value.Address;
            salesPerson.Manager = value.Manager;
            salesPerson.Phone = value.Phone;
            salesPerson.StartDate = value.StartDate;
            salesPerson.TerminationDate = value.TerminationDate;

            await _context.SaveChangesAsync();
            return await GetById(value.Id);
        }
        
        public async Task Remove(int id)
        {
            var salesPerson = await GetById(id);

            if (salesPerson != null)
            {
                _context.SalesPersons.Remove(salesPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}