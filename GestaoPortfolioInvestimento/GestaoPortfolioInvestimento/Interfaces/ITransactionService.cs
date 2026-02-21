using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Interfaces
{
    public interface ITransactionService
    {
        Transaction GetTransactionById(int id);

        List<Transaction> GetAllTransactions(int skip, int take);
    }
}