namespace DolarMoney.Model
{
    public class OperacaoModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idOperacao,
                    _idInvestimento;
        private DateTime _dataOperacao;
        private string _indice,
                       _cv;
        private decimal _quantidade,
                        _taxa,
                        _precoUnitario,
                        _valorTotal,
                        _valorTotalComTaxa;

        private int _numero;
        private string _dd_Mm_Yyyy_Operacao,
                       _ticker;
        private decimal _lucroPrejuizo,
                        _lucroPrejuizoPercentual;

        #endregion

        #region Construtor

        public OperacaoModel() { }

        #endregion

        #region Private

        private void calcular()
        {
            if (!string.IsNullOrWhiteSpace(CV) &&
                ValorTotal > 0 &&
                Quantidade > 0)
            {
                decimal dcmPrecoUnitario = 0;

                if (CV == "C")
                    dcmPrecoUnitario = (ValorTotal + Taxa) / Quantidade;
                else
                    dcmPrecoUnitario = (ValorTotal - Taxa) / Quantidade;

                PrecoUnitario = (decimal)(((int)(100 * dcmPrecoUnitario)) / 100.0);
            }
        }

        #endregion

        #region Propriedades

        public int IdOperacao
        {
            get => _idOperacao;
            set
            {
                if (_idOperacao != value)
                {
                    _idOperacao = value;
                    OnPropertyChanged(nameof(IdOperacao));
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
        public DateTime DataOperacao
        {
            get => _dataOperacao;
            set
            {
                if (_dataOperacao != value)
                {
                    _dataOperacao = value;
                    OnPropertyChanged(nameof(DataOperacao));

                    Dd_Mm_Yyyy_Operacao = Dd_Mm_Yyyy(_dataOperacao);
                }
            }
        }
        public string Indice
        {
            get => _indice;
            set
            {
                if (_indice != value)
                {
                    _indice = value;
                    OnPropertyChanged(nameof(Indice));
                }
            }
        }
        public string CV
        {
            get => _cv;
            set
            {
                if (_cv != value)
                {
                    _cv = value;
                    OnPropertyChanged(nameof(CV));
                }
            }
        }
        public decimal Quantidade
        {
            get => _quantidade;
            set
            {
                if (_quantidade != value)
                {
                    _quantidade = value;
                    OnPropertyChanged(nameof(Quantidade));

                    calcular();
                }
            }
        }
        public decimal Taxa
        {
            get => _taxa;
            set
            {
                if (_taxa != value)
                {
                    _taxa = value;
                    OnPropertyChanged(nameof(Taxa));
                }
            }
        }
        public decimal PrecoUnitario
        {
            get => _precoUnitario;
            set
            {
                if (_precoUnitario != value)
                {
                    _precoUnitario = value;
                    OnPropertyChanged(nameof(PrecoUnitario));
                }
            }
        }
        public decimal ValorTotal
        {
            get => _valorTotal;
            set
            {
                if (_valorTotal != value)
                {
                    _valorTotal = value;
                    OnPropertyChanged(nameof(ValorTotal));

                    calcular();
                }
            }
        }
        public decimal ValorTotalComTaxa
        {
            get => _valorTotalComTaxa;
            set
            {
                if (_valorTotalComTaxa != value)
                {
                    _valorTotalComTaxa = value;
                    OnPropertyChanged(nameof(ValorTotalComTaxa));

                    calcular();
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
        public string Dd_Mm_Yyyy_Operacao
        {
            get => _dd_Mm_Yyyy_Operacao;
            set
            {
                if (_dd_Mm_Yyyy_Operacao != value)
                {
                    _dd_Mm_Yyyy_Operacao = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Operacao));
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
        public decimal LucroPrejuizo
        {
            get => _lucroPrejuizo;
            set
            {
                if (_lucroPrejuizo != value)
                {
                    _lucroPrejuizo = value;
                    OnPropertyChanged(nameof(LucroPrejuizo));
                }
            }
        }
        public decimal LucroPrejuizoPercentual
        {
            get => _lucroPrejuizoPercentual;
            set
            {
                if (_lucroPrejuizoPercentual != value)
                {
                    _lucroPrejuizoPercentual = value;
                    OnPropertyChanged(nameof(LucroPrejuizoPercentual));
                }
            }
        }

        #endregion
    }
}