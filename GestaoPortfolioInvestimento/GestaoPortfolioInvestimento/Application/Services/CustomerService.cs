using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Infrastructure;

namespace InvestmentPortfolioManagement.Application.Services

{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public void AddCustomer(CustomerDTO customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer cannot be null");
            }

            var newCustomer = new CustomerEntity
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                DateOfBirth = customerDto.DateOfBirth,
                Address = customerDto.Address
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
        }

        public CustomerEntity GetCustomerById(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            return customer;
        }

        public List<CustomerEntity> GetAllCustomers(int skip, int take)
        {
            return _context.Customers.Skip(skip).Take(take).ToList();
        }

        public void UpdateCustomer(int id, CustomerDTO customerDto)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.DateOfBirth = customerDto.DateOfBirth;
            customer.Address = customerDto.Address;

            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
