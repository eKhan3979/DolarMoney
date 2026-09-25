namespace DolarMoney.Model.DTO
{
    public class CotacaoGraficoDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private DateTime _dataCotacao;
        private string _dd_Mm_Yyyy;
        private decimal? _valorCotacao;
        private string _valorCotacaoStr;

        #endregion

        #region Construtor

        public CotacaoGraficoDTO() { }

        #endregion

        #region Público

        public int Numero
        {
            get => _numero;
            set
            {
                _numero = value;
                OnPropertyChanged(nameof(Numero));
            }
        }
        public DateTime DataCotacao
        {
            get => _dataCotacao;
            set
            {
                _dataCotacao = value;
                OnPropertyChanged(nameof(DataCotacao));

                Dd_Mm_Yyyy = _dataCotacao.ToString("dd/MM/yyyy");
            }
        }
        public string Dd_Mm_Yyyy
        {
            get => _dd_Mm_Yyyy;
            set
            {
                _dd_Mm_Yyyy = value;
                OnPropertyChanged(nameof(Dd_Mm_Yyyy));
            }
        }
        public decimal? ValorCotacao
        {
            get => _valorCotacao;
            set
            {
                _valorCotacao = value;
                OnPropertyChanged(nameof(ValorCotacao));

                ValorCotacaoStr = ((_valorCotacao != null) ? _valorCotacao.Value.ToString("#,##0.#0") : "");
            }
        }
        public string ValorCotacaoStr
        {
            get => _valorCotacaoStr;
            set
            {
                _valorCotacaoStr = value;
                OnPropertyChanged(nameof(ValorCotacaoStr));
            }
        }

        #endregion
    }
}