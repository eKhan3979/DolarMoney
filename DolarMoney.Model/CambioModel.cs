namespace DolarMoney.Model
{
    public class CambioModel: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _dd_Mm_Yyyy_Exibir,
                       _yyyy_Mm_Dd;
        private decimal _compra,
                        _venda;

        #endregion

        #region Construtor

        public CambioModel() { }

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
        public string Dd_Mm_Yyyy_Exibir
        {
            get => _dd_Mm_Yyyy_Exibir;
            set
            {
                if (_dd_Mm_Yyyy_Exibir != value)
                {
                    _dd_Mm_Yyyy_Exibir = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Exibir));
                }
            }
        }
        public string Yyyy_Mm_Dd
        {
            get => _yyyy_Mm_Dd;
            set
            {
                if (_yyyy_Mm_Dd != value)
                {
                    _yyyy_Mm_Dd = value;

                    OnPropertyChanged(nameof(Yyyy_Mm_Dd));

                    if (_yyyy_Mm_Dd.Length == 10)
                        Dd_Mm_Yyyy_Exibir = _yyyy_Mm_Dd.Substring(8, 2) + "/" +
                                            _yyyy_Mm_Dd.Substring(5, 2) + "/" +
                                            _yyyy_Mm_Dd.Substring(0, 4);
                }
            }
        }
        public decimal Compra
        {
            get => _compra;
            set
            {
                if (_compra != value)
                {
                    _compra = value;
                    OnPropertyChanged(nameof(Compra));
                }
            }
        }
        public decimal Venda
        {
            get => _venda;
            set
            {
                if (_venda != value)
                {
                    _venda = value;
                    OnPropertyChanged(nameof(_venda));
                }
            }
        }

        #endregion
    }
}