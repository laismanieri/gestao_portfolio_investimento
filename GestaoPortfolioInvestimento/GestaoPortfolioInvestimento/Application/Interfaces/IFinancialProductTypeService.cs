using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Shared;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface IFinancialProductTypeService
    {

        Task<FinancialProductTypeDetailsResponse> CreateAsync(FinancialProductTypeCreateRequest request);

        Task<FinancialProductTypeDetailsResponse> GetFinancialProductTypeByGuidAsync(Guid guid);    

        Task UpdateAsync(Guid guid, FinancialProductTypeUpdateRequest request);

        Task DeleteAsync(Guid guid);    

        Task<List<FinancialProductTypeResponse>> GetAllFinancialProductTypeAsync(PaginationQuery query);

        Task<List<FinancialProductTypeDetailsResponse>> GetAllFinancialProductTypeAndFinancialProductAsync(PaginationQuery query);
    }
}
