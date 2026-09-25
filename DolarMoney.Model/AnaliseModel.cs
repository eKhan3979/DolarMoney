namespace DolarMoney.Model
{
    public class AnaliseModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idAnalise;
        private DateTime _dataAnalise;
        private int _idInvestimento,
                    _idTendencia;
        private DateTime? _dataCompra,
                       _dataVenda;
        private int _diasMM1,
                    _diasMM2;
        private short _expirado;

        #endregion

        #region Construtor

        public AnaliseModel() { }

        #endregion

        #region Propriedades

        public int IdAnalise
        {
            get => _idAnalise;
            set
            {
                if (_idAnalise != value)
                {
                    _idAnalise = value;
                    OnPropertyChanged(nameof(IdAnalise));
                }
            }
        }
        public DateTime DataAnalise
        {
            get => _dataAnalise;
            set
            {
                if (_dataAnalise != value)
                {
                    _dataAnalise = value;
                    OnPropertyChanged(nameof(DataAnalise));
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
        public DateTime? DataCompra
        {
            get => _dataCompra;
            set
            {
                if (_dataCompra != value)
                {
                    _dataCompra = value;
                    OnPropertyChanged(nameof(DataCompra));
                }
            }
        }
        public DateTime? DataVenda
        {
            get => _dataVenda;
            set
            {
                if (_dataVenda != value)
                {
                    _dataVenda = value;
                    OnPropertyChanged(nameof(DataVenda));
                }
            }
        }
        public int DiasMM1
        {
            get => _diasMM1;
            set
            {
                if (_diasMM1 != value)
                {
                    _diasMM1 = value;
                    OnPropertyChanged(nameof(DiasMM1));
                }
            }
        }
        public int DiasMM2
        {
            get => _diasMM2;
            set
            {
                if (_diasMM2 != value)
                {
                    _diasMM2 = value;
                    OnPropertyChanged(nameof(DiasMM2));
                }
            }
        }
        public short Expirado
        {
            get => _expirado;
            set
            {
                if (_expirado != value)
                {
                    _expirado = value;
                    OnPropertyChanged(nameof(Expirado));
                }
            }
        }

        #endregion
    }
}