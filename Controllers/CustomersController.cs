using Dot_Net_Core_Tutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomersController(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _context.Customers.ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customers customers)
        {
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            //var result = await _context.Customers.FindAsync(Id);
            var result = await _context.Customers.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCustomer(int Id, [FromBody] Customers updatedCustomer)
        {
            // Find the existing customer by Id
            //var customer = await _context.Customers.FindAsync(Id);
            var customer = await _context.Customers.Where(a => a.Id == Id).FirstOrDefaultAsync();

            if (customer == null)
                return NotFound();

            // Update properties — you can do this manually or use a mapper
            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            // ... update other fields as needed

            // Save changes to database
            await _context.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
