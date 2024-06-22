using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;
using System.ComponentModel.DataAnnotations;


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
        public IActionResult ObterTodosProdutosFinanceiros()
        {
            var produtosFinanceiros = _produtoFinanceiro.ObterTodosProdutosFinanceiros();
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
        public IActionResult AdicionarProdutoFinanceiro([FromBody] ProdutoFinanceiro produtoFinanceiro)
        {
            try
            {
                _produtoFinanceiro.AdicionarProdutoFinanceiro(produtoFinanceiro);
                return CreatedAtAction(nameof(ObterProdutoFinanceiroPorId), new { id = produtoFinanceiro.ID }, produtoFinanceiro);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarprodutoFinanceiro(int id, [FromBody] ProdutoFinanceiro produtoFinanceiro)
        {
            if (id != produtoFinanceiro.ID)
            {
                return BadRequest("ID do produtoFinanceiro não encontrado");
            }

            try
            {
                _produtoFinanceiro.AtualizarProdutoFinanceiro(produtoFinanceiro);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
