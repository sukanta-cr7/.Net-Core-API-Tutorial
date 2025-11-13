using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;
using Dot_Net_Core_Tutorial.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _Customer;
        public CustomersController(ICustomerService customerService) 
        {
            _Customer = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<CustomersDTO>> GetCustomers()
        {
            var result = await _Customer.GetCustomers();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CustomersDTO?>> CreateCustomer([FromBody] CustomersDTO customers)
        {
            var user = await _Customer.CreateCustomer(customers);
            if (user == null)
            {
                return BadRequest("User already exists.");
            }
            return Ok(user);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomersDTO>> GetCustomerById(int Id)
        {
            var result = await _Customer.GetCustomerById(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<CustomersDTO>> UpdateCustomer(int Id, [FromBody] CustomersDTO updatedCustomer)
        {
            var customer = await _Customer.UpdateCustomer(Id,updatedCustomer);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
    }
}
