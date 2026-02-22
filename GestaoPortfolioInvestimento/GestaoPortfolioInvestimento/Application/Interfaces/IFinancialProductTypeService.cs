using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Shared;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IFinancialProductTypeService
    {

        Task<FinancialProductTypeResponse> CreateAsync(FinancialProductTypeCreateRequest request);

        Task<FinancialProductTypeResponse> GetFinancialProductTypeByGuidAsync(Guid guid);    

        Task UpdateAsync(Guid guid, FinancialProductTypeUpdateRequest request);

        Task DeleteAsync(Guid guid);    

        Task<List<FinancialProductTypeResponse>> GetAllFinancialProductTypeAsync(PaginationQuery query);

        Task<List<FinancialProductTypeResponse>> GetAllFinancialProductTypeAndFinancialProductAsync(PaginationQuery query);
    }
}
