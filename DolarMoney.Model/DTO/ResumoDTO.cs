using System.Text;

namespace DolarMoney.Model.DTO
{
    public class ResumoDTO: BaseModel
    {
        #region Variáveis da Classe

        private decimal _lucro,
                        _prejuizo,
                        _liquido,
                        _valorEstoque,
                        _estoque,
                        _total,
                        _percentual;

        private string _lucroStr,
                       _prejuizoStr,
                       _liquidoStr,
                       _valorEstoqueStr,
                       _estoqueStr,
                       _totalStr,
                       _percentualStr;

        #endregion

        #region Construtor

        public ResumoDTO() { }

        #endregion

        #region Propriedades

        public decimal Lucro
        {
            get => _lucro;
            set
            {
                if (_lucro != value)
                {
                    _lucro = value;
                    OnPropertyChanged(nameof(Lucro));

                    LucroStr = _lucro.ToString("#,##0.#0");
                }
            }
        }
        public decimal Prejuizo
        {
            get => _prejuizo;
            set
            {
                if (_prejuizo != value)
                {
                    _prejuizo = value;
                    OnPropertyChanged(nameof(Prejuizo));

                    PrejuizoStr = _prejuizo.ToString("#,##0.#0");
                }
            }
        }
        public decimal Liquido
        {
            get => _liquido;
            set
            {
                if (_liquido != value)
                {
                    _liquido = value;
                    OnPropertyChanged(nameof(Liquido));

                    LiquidoStr = _liquido.ToString("#,##0.#0");
                }
            }
        }
        public decimal ValorEstoque
        {
            get => _valorEstoque;
            set
            {
                if (_valorEstoque != value)
                {
                    _valorEstoque = value;
                    OnPropertyChanged(nameof(ValorEstoque));

                    ValorEstoqueStr = _valorEstoque.ToString("#,##0.#0");
                }
            }
        }
        public decimal Estoque
        {
            get => _estoque;
            set
            {
                if (_estoque != value)
                {
                    _estoque = value;
                    OnPropertyChanged(nameof(Estoque));

                    EstoqueStr = _estoque.ToString("#,##0.#0");
                }
            }
        }
        public decimal Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));

                    TotalStr = _total.ToString("#,##0.#0");
                }
            }
        }
        public decimal Percentual
        {
            get => _percentual;
            set
            {
                if (_percentual != value)
                {
                    _percentual = value;
                    OnPropertyChanged(nameof(Percentual));

                    PercentualStr = _percentual.ToString("#,##0.#0%");
                }
            }
        }

        public string LucroStr
        {
            get => _lucroStr;
            set
            {
                if (_lucroStr != value)
                {
                    _lucroStr = value;
                    OnPropertyChanged(nameof(LucroStr));
                }
            }
        }
        public string PrejuizoStr
        {
            get => _prejuizoStr;
            set
            {
                if (_prejuizoStr != value)
                {
                    _prejuizoStr = value;
                    OnPropertyChanged(nameof(PrejuizoStr));
                }
            }
        }
        public string LiquidoStr
        {
            get => _liquidoStr;
            set
            {
                if (_liquidoStr != value)
                {
                    _liquidoStr = value;
                    OnPropertyChanged(nameof(LiquidoStr));
                }
            }
        }
        public string ValorEstoqueStr
        {
            get => _valorEstoqueStr;
            set
            {
                if (_valorEstoqueStr != value)
                {
                    _valorEstoqueStr = value;
                    OnPropertyChanged(nameof(ValorEstoqueStr));
                }
            }
        }
        public string EstoqueStr
        {
            get => _estoqueStr;
            set
            {
                if (_estoqueStr != value)
                {
                    _estoqueStr = value;
                    OnPropertyChanged(nameof(EstoqueStr));
                }
            }
        }
        public string TotalStr
        {
            get => _totalStr;
            set
            {
                if (_totalStr != value)
                {
                    _totalStr = value;
                    OnPropertyChanged(nameof(TotalStr));
                }
            }
        }
        public string PercentualStr
        {
            get => _percentualStr;
            set
            {
                if (_percentualStr != value)
                {
                    _percentualStr = value;
                    OnPropertyChanged(nameof(PercentualStr));
                }
            }
        }

        #endregion
    }
}