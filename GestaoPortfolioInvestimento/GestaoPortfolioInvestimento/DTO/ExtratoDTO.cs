namespace GestaoPortfolioInvestimento.DTO
{
    public class ExtratoDTO
    {
        public int ClienteID { get; set; }
        public string ClienteNome { get; set; }
        public List<InvestimentoDetalheDTO> Investimentos { get; set; } = new List<InvestimentoDetalheDTO>();
    }
}
