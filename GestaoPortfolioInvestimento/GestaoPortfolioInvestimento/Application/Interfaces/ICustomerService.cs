using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{ 
    public interface ICustomerService
    {
        void AddCustomer(CustomerDTO customerDto);

        CustomerEntity GetCustomerById(int id);

        List<CustomerEntity> GetAllCustomers(int skip, int take);

        void UpdateCustomer(int id, CustomerDTO customerDto);

        void DeleteCustomer(int id);
    }
}

