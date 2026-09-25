namespace DolarMoney.Model.DTO
{
    public class CotacaoPercentualDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _ticker;
        private int _idInvestimento;
        private int? _idCotacao;
        private decimal? _cotacao,
                         _cotacaoAnterior,
                         _percentual;
        private string _cotacaoStr,
                       _cotacaoAnteriorStr,
                       _colorStr,
                       _percentualStr;

        #endregion

        #region Construtor

        public CotacaoPercentualDTO() { }

        #endregion

        #region Propriedades

        public int Numero
        {
            get => _numero;
            set
            {
                if (_numero != value)
                {
                    _numero = value;
                    OnPropertyChanged(nameof(Numero));
                }
            }
        }
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
        public int IdInvestimento
        {
            get => _idInvestimento;
            set
            {
                if (_idInvestimento != value)
                {
                    _idInvestimento = value;
                    OnPropertyChanged(nameof(IdInvestimento));
                }
            }
        }
        public int? IdCotacao
        {
            get => _idCotacao;
            set
            {
                if (_idCotacao != value)
                {
                    _idCotacao = value;
                    OnPropertyChanged(nameof(IdCotacao));
                }
            }
        }
        public decimal? Cotacao
        {
            get => _cotacao;
            set
            {
                if (_cotacao != value)
                {
                    _cotacao = value;
                    OnPropertyChanged(nameof(Cotacao));
                }
            }
        }
        public decimal? CotacaoAnterior
        {
            get => _cotacaoAnterior;
            set
            {
                if (_cotacaoAnterior != value)
                {
                    _cotacaoAnterior = value;
                    OnPropertyChanged(nameof(CotacaoAnterior));
                }
            }
        }
        public decimal? Percentual
        {
            get => _percentual;
            set
            {
                if (_percentual != value)
                {
                    _percentual = value;
                    OnPropertyChanged(nameof(Percentual));
                }
            }
        }
        public string CotacaoStr
        {
            get => _cotacaoStr;
            set
            {
                if (_cotacaoStr != value)
                {
                    _cotacaoStr = value;
                    OnPropertyChanged(nameof(CotacaoStr));
                }
            }
        }
        public string CotacaoAnteriorStr
        {
            get => _cotacaoAnteriorStr;
            set
            {
                if (_cotacaoAnteriorStr != value)
                {
                    _cotacaoAnteriorStr = value;
                    OnPropertyChanged(nameof(CotacaoAnteriorStr));
                }
            }
        }
        public string PercentualStr
        {
            get => _percentualStr;
            set
            {
                if (_percentualStr != value)
                {
                    _percentualStr = value;
                    OnPropertyChanged(nameof(PercentualStr));
                }
            }
        }
        public string ColorStr
        {
            get => _colorStr;
            set
            {
                if (_colorStr != value)
                {
                    _colorStr = value;
                    OnPropertyChanged(nameof(ColorStr));
                }
            }
        }

        #endregion
    }
}