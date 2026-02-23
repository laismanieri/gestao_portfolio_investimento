using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface ICustomerSubscriptionService
    {
        Task<CustomerSubscriptionResponse> CreateAsync(CustomerSubscriptionCreateRequest request);

        Task<CustomerSubscriptionResponse> GetCustomerSubscriptionByGuidAsync(Guid guid);

        Task<CustomerSubscriptionResponse> GetCustomerSubscriptionByCustomerGuidAsync(Guid customerGuid);

        Task<List<CustomerSubscriptionResponse>> GetAllAsync(PaginationQuery query);

        // TODO
        //public CustomerStatementDTO GetCustomerStatementById(int customerId);

        Task<CustomerSubscriptionDetailResponse> GetDetailWithTransationsByGuidAsync(Guid guid);

        Task<Dictionary<Guid, List<CustomerSubscriptionResponse>>> GetListNearMaturityAsync(int days);

        Task UpdateAsync(Guid guid, CustomerSubscriptionUpdateRequest request);

        Task DeleteAsync(Guid guid);

        Task<Dictionary<Guid, List<CustomerSubscriptionDetailResponse>>> GetUpcomingInvestmentsForEmailAsync(int days);

    }
}
