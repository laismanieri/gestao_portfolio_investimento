using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using GestaoPortfolioInvestimento.Models;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {

        private readonly ITransacao _transacao;

        public TransacaoController(ITransacao transacao)
        {
            _transacao = transacao;
        }


        [HttpGet]
        public IActionResult ObterTodosClientes()
        {
            var transacoes = _transacao.ObterTodasTransacoes();
            return Ok(transacoes);
        }
    }

}
