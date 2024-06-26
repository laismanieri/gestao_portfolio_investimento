using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;
using GestaoPortfolioInvestimento.DTO;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoFinanceiroController : ControllerBase
    {

        private readonly IProdutoFinanceiro _produtoFinanceiro;

        public ProdutoFinanceiroController(IProdutoFinanceiro produtoFinanceiro)
        {
            _produtoFinanceiro = produtoFinanceiro;
        }


        [HttpGet]
        public IActionResult ObterTodosProdutosFinanceiros([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var produtosFinanceiros = _produtoFinanceiro.ObterTodosProdutosFinanceiros(skip, take);
            return Ok(produtosFinanceiros);
        }

        [HttpGet("{id}")]
        public IActionResult ObterProdutoFinanceiroPorId(int id)
        {
            try
            {
                var produtoFinanceiro = _produtoFinanceiro.ObterProdutoFinanceiroPorId(id);
                return Ok(produtoFinanceiro);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarProdutoFinanceiro([FromBody] ProdutoFinanceiroDTO produtoFinanceiroDto)
        {
            try
            {
                _produtoFinanceiro.AdicionarProdutoFinanceiro(produtoFinanceiroDto);
                return CreatedAtAction(nameof(ObterProdutoFinanceiroPorId), new { id = produtoFinanceiroDto.ID }, produtoFinanceiroDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarProdutoFinanceiro(int id, [FromBody] ProdutoFinanceiroDTO produtoFinanceiroDto)
        {
            try
            {
                _produtoFinanceiro.AtualizarProdutoFinanceiro(id, produtoFinanceiroDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverProdutoFinanceiro(int id)
        {
            try
            {
                _produtoFinanceiro.RemoverProdutoFinanceiro(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
