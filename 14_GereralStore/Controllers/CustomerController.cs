using _14_GereralStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace _14_GereralStore.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer()
        {
            List<Customer> customer = await _context.Customers.ToListAsync();
            return Ok(customer);
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerByID(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == default)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerByName(string firstName, string lastName)
        {

            //List<Customer> customers = await _context.Customers.Where(c => c.LastName.ToLower() == lastName.ToLower()).ToListAsync();
            //return Ok(customers);
            List<Customer> customers;
            if (firstName == default)
            {
                customers = await _context.Customers
                    .Where(c => c.LastName.ToLower() == lastName.ToLower())
                    .ToListAsync();
            } else if (lastName == default)
            {
                customers = await _context.Customers
                    .Where(c => c.FirstName.ToLower() == firstName.ToLower())
                    .ToListAsync();
            } else
            {
                customers = await _context.Customers
                    .Where(c => c.LastName.ToLower() == lastName.ToLower()
                    && c.FirstName.ToLower() == firstName.ToLower())
                    .ToListAsync();
            }
            return Ok(customers);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomers([FromUri] int id, [FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id); 

            if (customer == default)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                customer.FirstName = model.FirstName;
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                customer.LastName = model.LastName;
            }

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer == default)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok(); // 200
            }

            return InternalServerError();
        }
    }
}
