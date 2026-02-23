using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.DTOs.Transaction;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> GetTransactionByGuidAsync(Guid guid);

        Task<List<TransactionResponse>> GetAllTransactionsAsync(PaginationQuery query);
    }
}