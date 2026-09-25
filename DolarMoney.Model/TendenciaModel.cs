namespace DolarMoney.Model
{
    public class TendenciaModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idTendencia;
        private string _tendencia;

        #endregion

        #region Construtor

        public TendenciaModel() { }

        #endregion

        #region Propriedades

        public int IdTendencia
        {
            get => _idTendencia;
            set
            {
                if (_idTendencia != value)
                {
                    _idTendencia = value;
                    OnPropertyChanged(nameof(IdTendencia));
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