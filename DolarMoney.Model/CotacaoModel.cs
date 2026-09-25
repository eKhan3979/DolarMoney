namespace DolarMoney.Model
{
    public class CotacaoModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idCotacao,
                    _idInvestimento;
        private string _dataCotacao;
        private decimal? _cotacao,
                         _maximo,
                         _minimo,
                         _volume;

        private int _numero;
        private string _dd_Mm_Yyyy_Cotacao;

        #endregion

        #region Construtor

        public CotacaoModel() { }

        #endregion

        #region Propriedades

        public int IdCotacao
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
        public string DataCotacao
        {
            get => _dataCotacao;
            set
            {
                if (_dataCotacao != value)
                {
                    _dataCotacao = value;
                    OnPropertyChanged(nameof(DataCotacao));

                    Dd_Mm_Yyyy_Cotacao = Dd_Mm_Yyyy(_dataCotacao);
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
        public decimal? Maximo
        {
            get => _maximo;
            set
            {
                if (_maximo != value)
                {
                    _maximo = value;
                    OnPropertyChanged(nameof(Maximo));
                }
            }
        }
        public decimal? Minimo
        {
            get => _minimo;
            set
            {
                if (_minimo != value)
                {
                    _minimo = value;
                    OnPropertyChanged(nameof(Minimo));
                }
            }
        }
        public decimal? Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

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
        public string Dd_Mm_Yyyy_Cotacao
        {
            get => _dd_Mm_Yyyy_Cotacao;
            set
            {
                if (_dd_Mm_Yyyy_Cotacao != value)
                {
                    _dd_Mm_Yyyy_Cotacao = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Cotacao));
                }
            }
        }

        #endregion
    }
}