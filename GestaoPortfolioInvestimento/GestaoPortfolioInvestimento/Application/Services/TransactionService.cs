using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public List<TransactionEntity> GetAllTransactions(int skip, int take)
        {
            return _context.Transactions
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public TransactionEntity GetTransactionById(int id)
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