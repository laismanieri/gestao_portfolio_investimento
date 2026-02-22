using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace InvestmentPortfolioManagement.Application.Interfaces
{ 
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateAsync(CustomerCreateRequest request);

        Task<CustomerResponse> GetCustomerByGuidAsync(Guid guid);

        Task UpdateAsync(Guid guid, CustomerUpdateRequest request);

        Task DeleteAsync(Guid guid);

        Task<List<CustomerResponse>> GetAllCustomersAsync(PaginationQuery query);
    }
}

