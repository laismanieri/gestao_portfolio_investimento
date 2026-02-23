using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.DTOs.Transaction;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(DataContext context, IMapper mapper, ILogger<TransactionService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<TransactionResponse>> GetAllTransactionsAsync(PaginationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var transactions = await _context.Transactions
                .AsNoTracking()
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} Ttansactions from skip {Skip} take {Take}",
                transactions.Count, query.Skip, query.Take);

            return _mapper.Map<List<TransactionResponse>>(transactions);

        }

        public async Task<TransactionResponse> GetTransactionByGuidAsync(Guid guid)
        {
            var transaction = await GetTransactionEntityByGuidAsync(guid);
            _logger.LogDebug("Fetched Transaction {Guid}", guid);
            return _mapper.Map<TransactionResponse>(transaction);
        }

        private async Task<TransactionEntity> GetTransactionEntityByGuidAsync(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(guid));

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(c => c.Guid == guid);

            return transaction ?? throw new KeyNotFoundException($"Transaction with Guid {guid} not found");
        }
    }
}