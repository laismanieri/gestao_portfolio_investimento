using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;


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
        public IActionResult ObterTodosClientes()
        {
            var produtosFinanceiros = _produtoFinanceiro.ObterTodosProdutosFinanceiros();
            return Ok(produtosFinanceiros);
        }
    }

}
