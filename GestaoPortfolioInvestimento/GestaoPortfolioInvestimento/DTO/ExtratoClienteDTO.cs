namespace GestaoPortfolioInvestimento.DTO
{
    public class ExtratoClienteDTO
    {
        public int ClienteID { get; set; }
        public string ClienteNome { get; set; }
        public List<InvestimentoDetalheDTO> Investimentos { get; set; } = new List<InvestimentoDetalheDTO>();
    }
}
