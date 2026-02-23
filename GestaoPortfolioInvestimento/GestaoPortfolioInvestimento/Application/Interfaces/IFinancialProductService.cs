using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.Notifications;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IFinancialProductService
    {
        Task<FinancialProductResponse> CreateAsync(FinancialProductCreateRequest request);

        Task<FinancialProductResponse> GetFinancialProductByGuidAsync(Guid guid);

        Task UpdateAsync(Guid guid, FinancialProductUpdateRequest request);

        Task DeleteAsync(Guid guid);

        Task<List<FinancialProductResponse>> GetAllFinancialProductsAsync(PaginationQuery query);

        Task<List<UpcomingFinancialProductNotification>> GetProductsNearMaturityAsync(int days);
    }
}