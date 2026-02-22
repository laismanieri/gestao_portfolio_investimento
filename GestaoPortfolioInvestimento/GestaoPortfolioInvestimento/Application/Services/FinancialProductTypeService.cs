using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class FinancialProductTypeService : IFinancialProductTypeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FinancialProductTypeService> _logger;

        public FinancialProductTypeService(DataContext context, IMapper mapper, ILogger<FinancialProductTypeService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FinancialProductTypeDetailsResponse> CreateAsync(FinancialProductTypeCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            _logger.LogInformation("Creating Financial Product Type {Name}", request.Name);

            var exists = await _context.FinancialProductTypes
                .AnyAsync(t => t.Name == request.Name);

            if (exists)
                throw new InvalidOperationException("Type already exists");

            var financialProductType = _mapper.Map<FinancialProductTypeEntity>(request);
            await _context.AddAsync(financialProductType);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Financial Product Type {Guid} created successfully", financialProductType.Guid);

            return _mapper.Map<FinancialProductTypeDetailsResponse>(financialProductType);
        }
        public async Task<FinancialProductTypeDetailsResponse> GetFinancialProductTypeByGuidAsync(Guid guid)
        {
            var financialProductType = await GetFinancialProductTypeEntityByIdAsync(guid);
            _logger.LogDebug("Fetched Financial Product {Guid}", guid);
            return _mapper.Map<FinancialProductTypeDetailsResponse>(financialProductType);
        }

        public async Task<List<FinancialProductTypeResponse>> GetAllFinancialProductTypeAsync(PaginationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var financialProductTypes = await _context.FinancialProductTypes
                .AsNoTracking()
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} Financial Product Types from skip {Skip} take {Take}",
                    financialProductTypes.Count, query.Skip, query.Take);

            return _mapper.Map<List<FinancialProductTypeResponse>>(financialProductTypes);
        }

        public async Task<List<FinancialProductTypeDetailsResponse>> GetAllFinancialProductTypeAndFinancialProductAsync(PaginationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var types = await _context.FinancialProductTypes
                .AsNoTracking()
                .Include(t => t.FinancialProducts)
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            _logger.LogInformation(
                "Fetched {Count} Financial Product Types with products",
                types.Count);

            return _mapper.Map<List<FinancialProductTypeDetailsResponse>>(types);
        }

        public async Task UpdateAsync(Guid guid, FinancialProductTypeUpdateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var financialProductType = await GetFinancialProductTypeEntityByIdAsync(guid);
            _logger.LogInformation("Updating Financial Product Types  {Guid}", guid);

            _mapper.Map(request, financialProductType);
            financialProductType.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Financial Product Types {Guid} updated successfully", guid);
        }
        public async Task DeleteAsync(Guid guid)
        {
            var financialProductType = await GetFinancialProductTypeEntityByIdAsync(guid);
            _logger.LogInformation("Deleting Financial Product Type  {Guid}", guid);

            _context.FinancialProductTypes.Remove(financialProductType);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Financial Product Type {Guid} deleted successfully", guid);

        }

        private async Task<FinancialProductTypeEntity> GetFinancialProductTypeEntityByIdAsync(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(guid));

            var financialProductType = await _context.FinancialProductTypes
                .FirstOrDefaultAsync(f => f.Guid == guid);

            return financialProductType ?? throw new KeyNotFoundException($"Financial Product Types with Guid {guid} not found");
        }
    }
}
