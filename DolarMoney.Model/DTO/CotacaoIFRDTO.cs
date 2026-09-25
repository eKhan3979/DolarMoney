namespace DolarMoney.Model.DTO
{
    public class CotacaoIFRDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _dd_Mm_Yyyy_Cotacao;
        private decimal _ganho,
                        _perda,
                        _mediaGanhos,
                        _mediaPerdas,
                        _rs,
                        _ifr,
                        _valorCotacao;

        private string _valorCotacaoStr,
                       _ifrStr,
                       _rsStr;

        #endregion

        #region Construtor

        public CotacaoIFRDTO()
        {
        }

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
        public decimal Ganho
        {
            get => _ganho;
            set
            {
                if (_ganho != value)
                {
                    _ganho = value;
                    OnPropertyChanged(nameof(Ganho));
                }
            }
        }
        public decimal Perda
        {
            get => _perda;
            set
            {
                if (_perda != value)
                {
                    _perda = value;
                    OnPropertyChanged(nameof(Perda));
                }
            }
        }
        public decimal MediaGanhos
        {
            get => _mediaGanhos;
            set
            {
                if (_mediaGanhos != value)
                {
                    _mediaGanhos = value;
                    OnPropertyChanged(nameof(MediaGanhos));
                }
            }
        }
        public decimal MediaPerdas
        {
            get => _mediaPerdas;
            set
            {
                if (_mediaPerdas != value)
                {
                    _mediaPerdas = value;
                    OnPropertyChanged(nameof(MediaPerdas));
                }
            }
        }
        public decimal RS
        {
            get => _rs;
            set
            {
                if (_rs != value)
                {
                    _rs = value;
                    OnPropertyChanged(nameof(RS));
                }
            }
        }
        public decimal IFR
        {
            get => _ifr;
            set
            {
                if (_ifr != value)
                {
                    _ifr = value;
                    OnPropertyChanged(nameof(IFR));
                }
            }
        }
        public decimal ValorCotacao
        {
            get => _valorCotacao;
            set
            {
                if (_valorCotacao != value)
                {
                    _valorCotacao = value;
                    OnPropertyChanged(nameof(ValorCotacao));
                }
            }
        }

        public string ValorCotacaoStr
        {
            get => _valorCotacao.ToString("#,##0.#0");
        }
        public string IFRStr
        {
            get => _ifr.ToString("0.#0");
        }
        public string RSStr
        {
            get => _rs.ToString("0.#0");
        }

        #endregion
    }
}