using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Domain.Enums;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class CustomerSubscriptionService : ICustomerSubscriptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerSubscriptionService> _logger;

        public CustomerSubscriptionService(
            DataContext context,
            IMapper mapper,
            ILogger<CustomerSubscriptionService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerSubscriptionResponse> CreateAsync(CustomerSubscriptionCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Guid == request.CustomerGuid)
                    ?? throw new KeyNotFoundException("Customer not found");

                var product = await _context.FinancialProducts
                    .FirstOrDefaultAsync(p => p.Guid == request.FinancialProductGuid)
                    ?? throw new KeyNotFoundException("Financial product not found");

                if (!product.IsActive)
                    throw new InvalidOperationException("Product is inactive");

                if (product.Quantity <= 0)
                    throw new InvalidOperationException("Product is out of stock");

                if (request.Quantity > product.Quantity)
                    throw new InvalidOperationException("Insufficient stock available");

                var totalValue = request.Quantity * product.UnitValue;

                var subscription = new CustomerSubscription
                {
                    CustomerId = customer.Id,
                    FinancialProductId = product.Id,
                    Quantity = request.Quantity,
                    TotalValue = totalValue,
                    SaleDate = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _context.CustomerSubscriptions.AddAsync(subscription);

                product.Quantity -= request.Quantity;

                if (product.Quantity < 0)
                    throw new InvalidOperationException("Stock cannot be negative");

                var transaction = new Transaction
                {
                    Investment = subscription,
                    Quantity = request.Quantity,
                    UnitValue = product.UnitValue,
                    TotalValue = totalValue,
                    TransactionType = TransactionType.Buy,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Transactions.AddAsync(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return _mapper.Map<CustomerSubscriptionResponse>(subscription);
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<CustomerSubscriptionResponse> GetCustomerSubscriptionByGuidAsync(Guid guid)
        {
            var entity = await GetCustomerSubscriptionEntityByIdAsync(guid);
            return _mapper.Map<CustomerSubscriptionResponse>(entity);
        }

        public async Task<CustomerSubscriptionResponse> GetCustomerSubscriptionByCustomerGuidAsync(Guid customerGuid)
        {
            var entity = await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .AsNoTracking()
                .Where(s => s.Customer.Guid == customerGuid)
                .OrderByDescending(s => s.CreatedAt)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("Subscription not found for customer");

            return _mapper.Map<CustomerSubscriptionResponse>(entity);
        }

        public async Task<List<CustomerSubscriptionResponse>> GetAllAsync(PaginationQuery query)
        {
            var list = await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .AsNoTracking()
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            return _mapper.Map<List<CustomerSubscriptionResponse>>(list);
        }

        public async Task<Dictionary<Guid, List<CustomerSubscriptionResponse>>> GetListNearMaturityAsync(int days)
        {
            if (days <= 0)
                throw new ArgumentException("days must be > 0");

            var today = DateTime.UtcNow.Date;
            var limitDate = today.AddDays(days);

            var list = await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .Where(s =>
                    s.SaleDate == null &&
                    s.FinancialProduct.MaturityDate >= today &&
                    s.FinancialProduct.MaturityDate <= limitDate)
                .AsNoTracking()
                .ToListAsync();

            var mapped = _mapper.Map<List<CustomerSubscriptionResponse>>(list);

            return mapped
                .GroupBy(x => x.Customer.Guid)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public async Task<Dictionary<Guid, List<CustomerSubscriptionDetailResponse>>> GetUpcomingInvestmentsForEmailAsync(int days)
        {
            if (days <= 0)
                throw new ArgumentException("days must be > 0");

            var today = DateTime.UtcNow.Date;
            var limitDate = today.AddDays(days);

            var list = await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .Include(s => s.Transactions)
                .Where(s =>
                    s.SaleDate == null &&
                    s.FinancialProduct.MaturityDate >= today &&
                    s.FinancialProduct.MaturityDate <= limitDate)
                .AsNoTracking()
                .ToListAsync();

            var mapped = _mapper.Map<List<CustomerSubscriptionDetailResponse>>(list);

            return mapped
                .GroupBy(x => x.CustomerSubscription.Customer.Guid)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public async Task<CustomerSubscriptionDetailResponse> GetDetailWithTransationsByGuidAsync(Guid guid)
        {
            var entity = await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.Guid == guid)
                ?? throw new KeyNotFoundException("Subscription not found");

            return _mapper.Map<CustomerSubscriptionDetailResponse>(entity);
        }

        public async Task UpdateAsync(Guid guid, CustomerSubscriptionUpdateRequest request)
        {
            if (request.Quantity == null || request.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var subscription = await _context.CustomerSubscriptions
                    .FirstOrDefaultAsync(s => s.Guid == guid)
                    ?? throw new KeyNotFoundException("Subscription not found");

                if (subscription.Quantity <= 0)
                    throw new InvalidOperationException("No quantity available to sell");

                int quantityToSell = request.Quantity.Value;

                if (quantityToSell > subscription.Quantity)
                    throw new InvalidOperationException("Cannot sell more than owned quantity");

                var product = await _context.FinancialProducts
                    .FirstOrDefaultAsync(p => p.Id == subscription.FinancialProductId)
                    ?? throw new KeyNotFoundException("Financial product not found");

                subscription.Quantity -= quantityToSell;
                subscription.TotalValue = subscription.Quantity * product.UnitValue;
                subscription.UpdatedAt = DateTime.UtcNow;

                product.Quantity += quantityToSell;

                var transaction = new Transaction
                {
                    Investment = subscription,
                    Quantity = quantityToSell,
                    UnitValue = product.UnitValue,
                    TotalValue = quantityToSell * product.UnitValue,
                    TransactionType = TransactionType.Sell,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Transactions.AddAsync(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Guid guid)
        {
            var entity = await GetCustomerSubscriptionEntityByIdAsync(guid);
            _context.CustomerSubscriptions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<CustomerSubscription> GetCustomerSubscriptionEntityByIdAsync(Guid guid)
        {
            return await _context.CustomerSubscriptions
                .Include(s => s.Customer)
                .Include(s => s.FinancialProduct)
                    .ThenInclude(p => p.Type)
                .FirstOrDefaultAsync(s => s.Guid == guid)
                ?? throw new KeyNotFoundException("Subscription not found");
        }

    }
}
