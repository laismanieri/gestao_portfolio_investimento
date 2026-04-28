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
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Customer, CustomerSummaryResponse>();
            CreateMap<CustomerCreateRequest, Customer>();
            CreateMap<CustomerUpdateRequest, Customer>();

            CreateMap<FinancialProductType, FinancialProductTypeDetailsResponse>()
                .ForMember(dest => dest.FinancialProductEntity, opt => opt.MapFrom(src => src.FinancialProducts));
            CreateMap<FinancialProductType, FinancialProductTypeResponse>();
            CreateMap<FinancialProductTypeCreateRequest, FinancialProductType>();
            CreateMap<FinancialProductTypeUpdateRequest, FinancialProductType>();

            CreateMap<FinancialProduct, FinancialProductResponse>();
            CreateMap<FinancialProductCreateRequest, FinancialProduct>();
            CreateMap<FinancialProductUpdateRequest, FinancialProduct>();
            CreateMap<FinancialProduct, FinancialProductSummary>();
            CreateMap<FinancialProduct, FinancialProductSummaryResponse>();

            CreateMap<CustomerSubscription, CustomerSubscriptionResponse>();
            CreateMap<CustomerSubscription, CustomerSubscriptionDetailResponse>()
                .ForMember(dest => dest.CustomerSubscription, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid));

            CreateMap<Transaction, TransationSummaryResponse>();
            CreateMap<Transaction, TransactionResponse>();

        }
    }
}
