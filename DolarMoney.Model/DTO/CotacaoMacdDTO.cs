namespace DolarMoney.Model.DTO
{
    public class CotacaoMacdDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private DateTime _dataCotacao;
        private string _dd_Mm_Yyyy;
        private double _valorCotacao,
                       _valorEMA1,
                       _valorEMA2,
                       _valorEMAMACD,
                       _valorMACD,
                       _valorMedia1,
                       _valorMedia2;
        private string _valorCotacaoStr,
                       _valorEMA1Str,
                       _valorEMA2Str,
                       _valorEMAMACDStr,
                       _valorMACDStr,
                       _valorMedia1Str,
                       _valorMedia2Str;

        #endregion

        #region Construtor

        public CotacaoMacdDTO() { }

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
        public double ValorCotacao
        {
            get => _valorCotacao;
            set
            {
                _valorCotacao = value;
                OnPropertyChanged(nameof(ValorCotacao));

                ValorCotacaoStr = _valorCotacao.ToString("#,##0.#0");
            }
        }
        public double ValorEMA1
        {
            get => _valorEMA1;
            set
            {
                _valorEMA1 = value;
                OnPropertyChanged(nameof(ValorEMA1));

                ValorEMA1Str = _valorEMA1.ToString("#,##0.#0");
            }
        }
        public double ValorEMA2
        {
            get => _valorEMA2;
            set
            {
                _valorEMA2 = value;
                OnPropertyChanged(nameof(ValorEMA2));

                ValorEMA2Str = _valorEMA2.ToString("#,##0.#0");
            }
        }
        public double ValorEMAMACD
        {
            get => _valorEMAMACD;
            set
            {
                _valorEMAMACD = value;
                OnPropertyChanged(nameof(ValorEMAMACD));

                ValorEMAMACDStr = _valorEMAMACD.ToString("#,##0.#0");
            }
        }
        public double ValorMACD
        {
            get => _valorMACD;
            set
            {
                if (_valorMACD != value)
                {
                    _valorMACD = value;
                    OnPropertyChanged(nameof(ValorMACD));

                    ValorMACDStr = _valorMACD.ToString("#,##0.#0");
                }
            }
        }
        public double ValorMedia1
        {
            get => _valorMedia1;
            set
            {
                _valorMedia1 = value;
                OnPropertyChanged(nameof(ValorMedia1));

                ValorMedia1Str = _valorMedia1.ToString("#,##0.#0");
            }
        }
        public double ValorMedia2
        {
            get => _valorMedia2;
            set
            {
                _valorMedia2 = value;
                OnPropertyChanged(nameof(ValorMedia2));

                ValorMedia2Str = _valorMedia2.ToString("#,##0.#0");
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
        public string ValorEMA1Str
        {
            get => _valorEMA1Str;
            set
            {
                _valorEMA1Str = value;
                OnPropertyChanged(nameof(ValorEMA1Str));
            }
        }
        public string ValorEMA2Str
        {
            get => _valorEMA2Str;
            set
            {
                _valorEMA2Str = value;
                OnPropertyChanged(nameof(ValorEMA2Str));
            }
        }
        public string ValorEMAMACDStr
        {
            get => _valorEMAMACDStr;
            set
            {
                _valorEMAMACDStr = value;
                OnPropertyChanged(nameof(ValorEMAMACDStr));
            }
        }
        public string ValorMACDStr
        {
            get => _valorMACDStr;
            set
            {
                if (_valorMACDStr != value)
                {
                    _valorMACDStr = value;
                    OnPropertyChanged(nameof(ValorMACDStr));
                }
            }
        }
        public string ValorMedia1Str
        {
            get => _valorMedia1Str;
            set
            {
                _valorMedia1Str = value;
                OnPropertyChanged(nameof(ValorMedia1Str));
            }
        }
        public string ValorMedia2Str
        {
            get => _valorMedia2Str;
            set
            {
                _valorMedia2Str = value;
                OnPropertyChanged(nameof(ValorMedia2Str));
            }
        }

        #endregion

    }
}