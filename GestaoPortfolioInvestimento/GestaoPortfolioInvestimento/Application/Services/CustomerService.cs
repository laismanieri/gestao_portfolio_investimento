using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Domain.Entities;
using InvestmentPortfolioManagement.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace InvestmentPortfolioManagement.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(DataContext context, IMapper mapper, ILogger<CustomerService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerResponse> CreateAsync(CustomerCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            _logger.LogInformation("Creating customer {Name}", request.Name);

            var customer = _mapper.Map<CustomerEntity>(request);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer {Guid} created successfully", customer.Guid);

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> GetCustomerByGuidAsync(Guid guid)
        {
            var customer = await GetCustomerEntityByGuidAsync(guid);
            _logger.LogDebug("Fetched customer {Guid}", guid);  
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<List<CustomerResponse>> GetAllCustomersAsync(PaginationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var customers = await _context.Customers
                .AsNoTracking()
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} customers from skip {Skip} take {Take}",
                customers.Count, query.Skip, query.Take);

            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task UpdateAsync(Guid guid, CustomerUpdateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var customer = await GetCustomerEntityByGuidAsync(guid);
            _logger.LogInformation("Updating customer {Guid}", guid);

            _mapper.Map(request, customer);
            customer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Customer {Guid} updated successfully", guid);
        }
       public async Task DeleteAsync(Guid guid)
        {
            var customer = await GetCustomerEntityByGuidAsync(guid);
            _logger.LogInformation("Deleting customer {Guid}", guid);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer {Guid} deleted successfully", guid);
        }

        private async Task<CustomerEntity> GetCustomerEntityByGuidAsync(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(guid));

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Guid == guid);

            return customer ?? throw new KeyNotFoundException($"Customer with Guid {guid} not found");
        }
    }
}
