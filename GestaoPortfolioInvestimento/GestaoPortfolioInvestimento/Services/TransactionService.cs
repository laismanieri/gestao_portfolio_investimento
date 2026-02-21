using InvestmentPortfolioManagement.Data;
using InvestmentPortfolioManagement.Interfaces;
using InvestmentPortfolioManagement.Models;

namespace InvestmentPortfolioManagement.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactions(int skip, int take)
        {
            return _context.Transactions
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Transaction GetTransactionById(int id)
        {
            var transaction = _context.Transactions
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with ID {id} was not found.");
            }

            return transaction;
        }
    }
}