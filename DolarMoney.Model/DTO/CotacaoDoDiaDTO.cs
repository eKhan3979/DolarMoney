using System.Diagnostics;

namespace DolarMoney.Model.DTO
{
    public class CotacaoDoDiaDTO: CotacaoModel
    {
        #region Variáveis da Classe

        private string _ticker = "";

        #endregion

        #region Construtor

        public CotacaoDoDiaDTO() { }

        public CotacaoDoDiaDTO(CotacaoModel cotacao) 
        {
            this.IdCotacao = cotacao.IdCotacao;
            this.IdInvestimento = cotacao.IdInvestimento;
            this.DataCotacao = cotacao.DataCotacao;
            this.Cotacao = cotacao.Cotacao;
        }

        #endregion

        #region Propriedades

        public string Ticker
        {
            get => _ticker;
            set
            {
                if (_ticker != value)
                {
                    _ticker = value;
                    OnPropertyChanged(nameof(Ticker));
                }
            }
        }

        #endregion
    }
}