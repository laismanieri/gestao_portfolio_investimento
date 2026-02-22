using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProductType;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialProductTypeController : ControllerBase
    {
        private readonly IFinancialProductTypeService _financialProductTypeService;

        public FinancialProductTypeController(IFinancialProductTypeService financialProductTypeService)
        {
            _financialProductTypeService = financialProductTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFinancialProductType([FromQuery] PaginationQuery query)
        {
            var financialProductTypes = await _financialProductTypeService.GetAllFinancialProductTypeAsync(query);
            return Ok(financialProductTypes);
        }

        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllFinancialProductTypeAndFinancialProduct([FromQuery] PaginationQuery query) 
        {
            var productTypesAndFinancialProducts = await _financialProductTypeService.GetAllFinancialProductTypeAndFinancialProductAsync(query);
            return Ok(productTypesAndFinancialProducts);

        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> GetFinancialProductTypeByGuid(Guid guid)
        {
            var financialProductType = await _financialProductTypeService.GetFinancialProductTypeByGuidAsync(guid);
            return Ok(financialProductType);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinancialProductType([FromBody] FinancialProductTypeCreateRequest request)
        {
            var createdFinancialProductType = await _financialProductTypeService.CreateAsync(request);
            return CreatedAtAction(nameof(GetFinancialProductTypeByGuid), new { guid = createdFinancialProductType.Guid }, createdFinancialProductType);

        }
    }
}
