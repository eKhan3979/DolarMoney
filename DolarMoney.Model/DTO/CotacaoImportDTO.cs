namespace DolarMoney.Model.DTO
{
    public class CotacaoImportDTO: BaseModel
    {
        #region Variáveis da Classe

        private decimal _price;
        private string _date;

        private int _numero,
                    _idCotacao;
        private string _valorCotacao,
                       _dd_Mm_Yyyy_Cotacao;

        #endregion

        #region Construtor

        public CotacaoImportDTO() { }

        #endregion

        #region Propriedades

        public decimal price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(price));

                    ValorCotacao = _price.ToString("#,##0.#0");
                }
            }
        }
        public string date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(date));

                    Dd_Mm_Yyyy_Cotacao = _date.Trim().Substring(0, 6) + "20" + _date.Trim().Substring(6, 2);
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
        public string ValorCotacao
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
