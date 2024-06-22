using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentoController : ControllerBase
    {

        private readonly IInvestimento _investimento;

        public InvestimentoController(IInvestimento investimento)
        {
            _investimento = investimento;
        }


        [HttpGet]
        public IActionResult ObterTodosClientes()
        {
            var investimentos = _investimento.ObterTodosInvestimentos();
            return Ok(investimentos);
        }
    }

}
