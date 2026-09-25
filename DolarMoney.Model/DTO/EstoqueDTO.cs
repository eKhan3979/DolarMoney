namespace DolarMoney.Model.DTO
{
    public class EstoqueDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _ticker,
                       _nome;
        private int _idInvestimento;
        private string _dataOperacao;
        private decimal _quantidade,
                        _valorTotal,
                        _custoUnitario;
        private decimal? _cotaAtual,
                         _totalAtual;
        private decimal _lucroPrejuizo,
                        _lucroPrejuizoPercentual;

        private string _quantidadeStr,
                       _valorTotalStr,
                       _custoUnitarioStr,
                       _cotaAtualStr,
                       _totalAtualStr,
                       _lucroPrejuizoStr,
                       _lucroPrejuizopercentualStr;

        #endregion

        #region Construtor

        public EstoqueDTO() { }

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
        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Nome));
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
        public string DataOperacao
        {
            get => _dataOperacao;
            set
            {
                if (_dataOperacao != value)
                {
                    _dataOperacao = value;
                    OnPropertyChanged(nameof(DataOperacao));
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

                    QuantidadeStr = _quantidade.ToString("#,##0.####0");
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

                    ValorTotalStr = _valorTotal.ToString("#,##0.#0");
                }
            }
        }
        public decimal CustoUnitario
        {
            get => _custoUnitario;
            set
            {
                if (_custoUnitario != value)
                {
                    _custoUnitario = value;
                    OnPropertyChanged(nameof(CustoUnitario));

                    CustoUnitarioStr = _custoUnitario.ToString("#,##0.#0");
                }
            }
        }
        public decimal? CotaAtual
        {
            get => _cotaAtual;
            set
            {
                if (_cotaAtual != value)
                {
                    _cotaAtual = value;
                    OnPropertyChanged(nameof(CotaAtual));

                    CotaAtualStr = ((_cotaAtual != null) ? _cotaAtual?.ToString("#,##0.#0") : "");
                }
            }
        }
        public decimal? TotalAtual
        {
            get => _totalAtual;
            set
            {
                if (_totalAtual != value)
                {
                    _totalAtual = value;
                    OnPropertyChanged(nameof(TotalAtual));

                    TotalAtualStr = ((_totalAtual != null) ? _totalAtual?.ToString("#,##0.#0") : "");
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

                    LucroPrejuizoStr = _lucroPrejuizo.ToString("#,##0.#0");
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

                    LucroPrejuizoPercentualStr = _lucroPrejuizoPercentual.ToString("#,##0.#0%");
                }
            }
        }

        public string QuantidadeStr
        {
            get => _quantidadeStr;
            set
            {
                if (_quantidadeStr != value)
                {
                    _quantidadeStr = value;
                    OnPropertyChanged(nameof(QuantidadeStr));
                }
            }
        }
        public string ValorTotalStr
        {
            get => _valorTotalStr;
            set
            {
                if (_valorTotalStr != value)
                {
                    _valorTotalStr = value;
                    OnPropertyChanged(nameof(ValorTotalStr));
                }
            }
        }
        public string CustoUnitarioStr
        {
            get => _custoUnitarioStr;
            set
            {
                if (_custoUnitarioStr != value)
                {
                    _custoUnitarioStr = value;
                    OnPropertyChanged(nameof(CustoUnitarioStr));
                }
            }
        }
        public string CotaAtualStr
        {
            get => _cotaAtualStr;
            set
            {
                if (_cotaAtualStr != value)
                {
                    _cotaAtualStr = value;
                    OnPropertyChanged(nameof(CotaAtualStr));
                }
            }
        }
        public string TotalAtualStr
        {
            get => _totalAtualStr;
            set
            {
                if (_totalAtualStr != value)
                {
                    _totalAtualStr = value;
                    OnPropertyChanged(nameof(TotalAtualStr));
                }
            }
        }
        public string LucroPrejuizoStr
        {
            get => _lucroPrejuizoStr;
            set
            {
                if (_lucroPrejuizoStr != value)
                {
                    _lucroPrejuizoStr = value;
                    OnPropertyChanged(nameof(LucroPrejuizoStr));
                }
            }
        }
        public string LucroPrejuizoPercentualStr
        {
            get => _lucroPrejuizopercentualStr;
            set
            {
                if (_lucroPrejuizopercentualStr != value)
                {
                    _lucroPrejuizopercentualStr = value;
                    OnPropertyChanged(nameof(LucroPrejuizoPercentualStr));
                }
            }
        }

        #endregion
    }
}