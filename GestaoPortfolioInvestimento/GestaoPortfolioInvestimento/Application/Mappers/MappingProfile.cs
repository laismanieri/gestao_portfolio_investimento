using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
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

            CreateMap<FinancialProductTypeEntity, FinancialProductTypeDetailsResponse>()
                .ForMember(dest => dest.FinancialProductEntity, opt => opt.MapFrom(src => src.FinancialProducts));
            CreateMap<FinancialProductTypeEntity, FinancialProductTypeResponse>();
            CreateMap<FinancialProductTypeCreateRequest, FinancialProductTypeEntity>();
            CreateMap<FinancialProductTypeUpdateRequest, FinancialProductTypeEntity>();

            CreateMap<FinancialProductEntity, FinancialProductResponse>();
            CreateMap<FinancialProductCreateRequest, FinancialProductEntity>();
            CreateMap<FinancialProductUpdateRequest, FinancialProductEntity>();
            CreateMap<FinancialProductEntity, FinancialProductSummary>();
        }
    }
}
