using GestaoPortfolioInvestimento.Models;
using System


namespace GestaoPortfolioInvestimento.Interfaces
{ 
    public interface ICliente
    {
        public void AdicionarCliente(Cliente cliente);

        public Cliente ObterClientePorID(int id);

        public List<Cliente> ObterTodosClientes();

        public void AtualizarCliente(Cliente cliente);

        public void RemoverCliente(int id);
    }
}

