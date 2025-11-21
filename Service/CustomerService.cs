using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_Tutorial.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(AppDbContext appDbContext, ILogger<CustomerService> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }
        public async Task<List<CustomersDTO>> GetCustomers()
        {
            _logger.LogInformation("To fetch all customer details");
            IQueryable<Customers> customers = _context.Customers;
            var result = await customers.OrderBy(a=> a.Name).Select(c => 
                new CustomersDTO
                {
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                }).ToListAsync();
            return result;
        }
        public async Task<CustomersDTO?> CreateCustomer(CustomersDTO customersDto)
        {
            IQueryable<Customers> customers = _context.Customers;
            if (await customers.AnyAsync(a => a.Email == customersDto.Email))
                return null;
            var customer = new Customers();
            customer.Name = customersDto.Name;
            customer.Email = customersDto.Email;
            customer.Phone = customersDto.Phone;
            customer.Address = customersDto.Address;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomersDTO
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
            };
        }
        public async Task<CustomersDTO?> GetCustomerById(int Id)
        {
            //var result = await _context.Customers.FindAsync(Id);
            IQueryable<Customers> customers = _context.Customers;
            var result = await customers.Where(a => a.Id == Id).Select(c =>
                new CustomersDTO
                {
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                }).FirstOrDefaultAsync();
            if (result == null)
                return null;
            return result;
        }
        public async Task<CustomersDTO?> UpdateCustomer(int Id, CustomersDTO updatedCustomer)
        {
            //var customer = await _context.Customers.FindAsync(Id);
            IQueryable<Customers> objCustomer = _context.Customers;
            var customer = await objCustomer.Where(a => a.Id == Id).FirstOrDefaultAsync();

            if (customer == null)
                return null;

            // Update properties — you can do this manually or use a mapper
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;
            customer.Address = updatedCustomer.Address;

            await _context.SaveChangesAsync();

            return new CustomersDTO
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
            };
        }
    }
}
