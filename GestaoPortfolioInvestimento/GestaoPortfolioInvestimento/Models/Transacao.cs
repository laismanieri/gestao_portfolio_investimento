using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolioInvestimento.Models
{
    public class Transacao
    {
        public int ID { get; set; }
        public int InvestimentoID { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorUnitario { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        // Relacionamento
        [ForeignKey("InvestimentoID")]
        public Investimento Investimento { get; set; }
    }
}