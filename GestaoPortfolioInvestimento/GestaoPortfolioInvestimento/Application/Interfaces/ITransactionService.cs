using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Interfaces
{
    public interface ITransactionService
    {
        TransactionEntity GetTransactionById(int id);

        List<TransactionEntity> GetAllTransactions(int skip, int take);
    }
}