using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, CustomerResponse>();
            CreateMap<CustomerCreateRequest, CustomerEntity>();
            CreateMap<CustomerUpdateRequest, CustomerEntity>();
        }
    }
}
