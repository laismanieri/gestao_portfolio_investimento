using InvestmentPortfolioManagement.Application.DTOs;
using InvestmentPortfolioManagement.Application.DTOs.Customer;
using InvestmentPortfolioManagement.Application.DTOs.FinancialProduct;
using InvestmentPortfolioManagement.Application.DTOs.Shared;
using InvestmentPortfolioManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialProductController : ControllerBase
    {
        private readonly IFinancialProductService _financialProductService;

        public FinancialProductController(IFinancialProductService financialProductService)
        {
            _financialProductService = financialProductService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialProductResponse>>> GetAllFinancialProducts([FromQuery] PaginationQuery query)
        {
            if (query.Skip < 0 || query.Take <= 0)
                return BadRequest("Invalid pagination parameters.");

            var products = await _financialProductService.GetAllFinancialProductsAsync(query);
            return Ok(products);
        }

        [HttpGet("{guid:guid}")]
        public async Task<IActionResult> GetFinancialProductByGuid(Guid guid)
        {
            var product = await _financialProductService.GetFinancialProductByGuidAsync(guid);
            return Ok(product);
        }

        [HttpGet("inactive-products")]
        public async Task<ActionResult<List<FinancialProductResponse>>> GetInactiveProducts()
        {
            var products = await _financialProductService.GetInactiveProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinancialProduct([FromBody] FinancialProductCreateRequest request)
        {
            var createdProduct = await _financialProductService.CreateAsync(request);
            return CreatedAtAction(nameof(GetFinancialProductByGuid), new { guid = createdProduct.Guid }, createdProduct);
        }


        [HttpPut("{guid:guid}")]
        public async Task<IActionResult> UpdateFinancialProduct(Guid guid, [FromBody] FinancialProductUpdateRequest request)
        {
            await _financialProductService.UpdateAsync(guid, request);
            return NoContent();
        }

        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> DeleteFinancialProduct(Guid guid)
        {
            await _financialProductService.DeleteAsync(guid);
            return NoContent();
        }
    }
}