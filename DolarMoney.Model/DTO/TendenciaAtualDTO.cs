namespace DolarMoney.Model.DTO
{
    public class TendenciaAtualDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _ticker = "",
                       _tendencia = "";
        private int _idInvestimento,
                    _idAnalise;
        private DateTime _dataAnalise;
        private int _idTendencia;
        private DateTime? _dataCompra,
                          _dataVenda;
        private int _diasMM1,
                    _diasMM2;
        private short _expirado;

        private string _dd_Mm_Yyyy_Analise,
                       _dd_Mm_Yyyy_Compra,
                       _dd_Mm_Yyyy_Venda;

        #endregion

        #region Construtor

        public TendenciaAtualDTO() { }

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
                    
                    Dd_Mm_Yyyy_Analise = _dataAnalise.ToString("dd/MM/yyyy");
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

                    if ((_dataCompra != null) && (_dataCompra.Value.Year > 2024))
                        Dd_Mm_Yyyy_Compra = _dataCompra.Value.ToString("dd/MM/yyyy");
                    else
                        Dd_Mm_Yyyy_Compra = "";
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

                    if ((_dataVenda != null) && (_dataVenda.Value.Year > 2024))
                        Dd_Mm_Yyyy_Venda = _dataVenda.Value.ToString("dd/MM/yyyy");
                    else
                        Dd_Mm_Yyyy_Venda = "";
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

        public string Dd_Mm_Yyyy_Analise
        {
            get => _dd_Mm_Yyyy_Analise;
            set
            {
                if (_dd_Mm_Yyyy_Analise != value)
                {
                    _dd_Mm_Yyyy_Analise = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Analise));
                }
            }
        }
        public string Dd_Mm_Yyyy_Compra
        {
            get => _dd_Mm_Yyyy_Compra;
            set
            {
                if (_dd_Mm_Yyyy_Compra != value)
                {
                    _dd_Mm_Yyyy_Compra = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Compra));
                }
            }
        }
        public string Dd_Mm_Yyyy_Venda
        {
            get => _dd_Mm_Yyyy_Venda;
            set
            {
                if (_dd_Mm_Yyyy_Venda != value)
                {
                    _dd_Mm_Yyyy_Venda = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Venda));
                }
            }
        }

        #endregion
    }
}