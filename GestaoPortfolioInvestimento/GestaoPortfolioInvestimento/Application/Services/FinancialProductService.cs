using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class FinancialProductService : IFinancialProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FinancialProductService> _logger;

        public FinancialProductService(DataContext context, IMapper mapper, ILogger<FinancialProductService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FinancialProductResponse> CreateAsync(FinancialProductCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            _logger.LogInformation("Creating financial product {Name}", request.Name);

            var type = await _context.FinancialProductTypes
                .FirstOrDefaultAsync(t => t.Guid == request.FinancialProductTypeGuid);

            if (type == null)
                throw new KeyNotFoundException($"Financial product type with Guid '{request.FinancialProductTypeGuid}' not found");

            if (await _context.FinancialProducts.AnyAsync(p => p.Name == request.Name && p.FinancialProductTypeId == type.Id))
                throw new InvalidOperationException($"Financial product {request.Name} already exists for type {type.Name}");

            var financialProduct = _mapper.Map<FinancialProductEntity>(request);
            financialProduct.FinancialProductTypeId = type.Id; 

            await _context.FinancialProducts.AddAsync(financialProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Financial product {Guid} created successfully", financialProduct.Guid);

            return _mapper.Map<FinancialProductResponse>(financialProduct);
        }

        public async Task<FinancialProductResponse> GetFinancialProductByGuidAsync(Guid guid)
        {
            var product = await GetFinancialProductEntityByGuidAsync(guid);
            _logger.LogDebug("Fetched financial product {Guid}", guid);
            return _mapper.Map<FinancialProductResponse>(product);
        }

        public async Task<List<FinancialProductResponse>> GetAllFinancialProductsAsync(PaginationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var products = await _context.FinancialProducts
                .Include(p => p.Type) 
                .AsNoTracking()
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} financial products from skip {Skip} take {Take}",
                products.Count, query.Skip, query.Take);

            return _mapper.Map<List<FinancialProductResponse>>(products);
        }

        public async Task UpdateAsync(Guid guid, FinancialProductUpdateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var product = await GetFinancialProductEntityByGuidAsync(guid);

            _logger.LogInformation("Updating financial product {Guid}", guid);

            if (request.FinancialProductTypeGuid != null)
            {
                var type = await _context.FinancialProductTypes
                    .FirstOrDefaultAsync(t => t.Guid == request.FinancialProductTypeGuid);

                if (type == null)
                    throw new KeyNotFoundException($"Financial product type {request.FinancialProductTypeGuid} not found");

                product.FinancialProductTypeId = type.Id;
            }

            _mapper.Map(request, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Financial product {Guid} updated successfully", guid);
        }

        public async Task DeleteAsync(Guid guid)
        {
            var product = await GetFinancialProductEntityByGuidAsync(guid);

            _logger.LogInformation("Deleting financial product {Guid}", guid);

            _context.FinancialProducts.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Financial product {Guid} deleted successfully", guid);
        }

        private async Task<FinancialProductEntity> GetFinancialProductEntityByGuidAsync(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(guid));

            var product = await _context.FinancialProducts
                .Include(p => p.Type)
                .FirstOrDefaultAsync(p => p.Guid == guid);

            return product ?? throw new KeyNotFoundException($"Financial product with Guid {guid} not found");
        }
    }
}