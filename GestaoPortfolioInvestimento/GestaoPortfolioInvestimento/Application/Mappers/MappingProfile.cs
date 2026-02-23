using AutoMapper;
using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.CustomerSubscription;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Transaction;
using InvestmentPortfolioManagement.Domain.Entities;

namespace InvestmentPortfolioManagement.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, CustomerResponse>();
            CreateMap<CustomerEntity, CustomerSummaryResponse>();
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
            CreateMap<FinancialProductEntity, FinancialProductSummaryResponse>();

            CreateMap<CustomerSubscriptionEntity, CustomerSubscriptionResponse>();
            CreateMap<CustomerSubscriptionEntity, CustomerSubscriptionDetailResponse>()
                .ForMember(dest => dest.CustomerSubscription, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid));

            CreateMap<TransactionEntity, TransationSummaryResponse>();
            CreateMap<TransactionEntity, TransactionResponse>();

        }
    }
}
