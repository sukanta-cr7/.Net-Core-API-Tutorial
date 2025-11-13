using Dot_Net_Core_Tutorial.DTOs;
using Dot_Net_Core_Tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_Tutorial.Service
{
    public interface ICustomerService
    {
        public Task<List<CustomersDTO>> GetCustomers();
        public Task<CustomersDTO?> CreateCustomer(CustomersDTO customers);
        public Task<CustomersDTO?> GetCustomerById(int Id);
        public Task<CustomersDTO?> UpdateCustomer(int Id, CustomersDTO updatedCustomer);
    }
}
