using System;
using GestaoPortfolioInvestimento.Models;

namespace GestaoPortfolioInvestimento.Interfaces
{ 
    public interface ICliente
    {
        public void AdicionarCliente(Cliente cliente);

        public Cliente ObterClientePorId(int id);

        public List<Cliente> ObterTodosClientes(int skip, int take);

        public void AtualizarCliente(Cliente cliente);

        public void RemoverCliente(int id);
    }
}

