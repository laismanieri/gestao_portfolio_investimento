using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.DTOs.Transaction;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> GetTransactionByGuidAsync(Guid guid);

        Task<List<TransactionResponse>> GetAllTransactionsAsync(PaginationQuery query);
    }
}