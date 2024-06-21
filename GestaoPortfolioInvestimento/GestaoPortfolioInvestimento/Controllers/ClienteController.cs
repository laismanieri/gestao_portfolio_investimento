using System;
using Microsoft.AspNetCore.Mvc;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;


namespace GestaoPortfolioInvestimento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente)
        {
            _cliente = cliente;
        }


        [HttpGet]
        public IActionResult ObterTodosClientes()
        {
            var clientes = _cliente.ObterTodosClientes();
            return Ok(clientes);
        }
    }

}
