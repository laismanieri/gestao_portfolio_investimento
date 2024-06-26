using System;
using GestaoPortfolioInvestimento.DTO;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{ 
    public interface ICliente
    {
        public void AdicionarCliente(ClienteDTO clienteDto);

        public Cliente ObterClientePorId(int id);

        public List<Cliente> ObterTodosClientes(int skip, int take);

        public void AtualizarCliente(int id, ClienteDTO clienteDto);

        public void RemoverCliente(int id);
    }
}

