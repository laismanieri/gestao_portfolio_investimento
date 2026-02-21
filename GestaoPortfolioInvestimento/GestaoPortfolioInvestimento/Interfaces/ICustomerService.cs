using InvestmentPortfolioManagement.DTO;
using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Interfaces
{ 
    public interface ICustomerService
    {
        void AddCustomer(CustomerDTO customerDto);

        Customer GetCustomerById(int id);

        List<Customer> GetAllCustomers(int skip, int take);

        void UpdateCustomer(int id, CustomerDTO customerDto);

        void DeleteCustomer(int id);
    }
}

