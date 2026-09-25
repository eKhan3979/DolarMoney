namespace DolarMoney.Model.DTO
{
    public class AnaliseDTO: AnaliseModel
    {
        #region Variáveis da Classe

        private int _numero;

        private string _ticker,
                       _tendencia;

        #endregion

        #region Construtor

        public AnaliseDTO() { }

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
        public string Tendencia
        {
            get => _tendencia;
            set
            {
                if (_tendencia != value)
                {
                    _tendencia = value;
                    OnPropertyChanged(nameof(Tendencia));
                }
            }
        }

        #endregion
    }
}