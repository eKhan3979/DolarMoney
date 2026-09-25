using System.Timers;

namespace DolarMoney.Model.DTO
{
    public class OperacaoCambioDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero;
        private string _dataOperacao;
        private int _idOperacao,
                    _idInvestimento;
        private string _cv;
        private decimal _quantidade,
                        _taxa,
                        _valorTotal,
                        _precoUnitario;
        private string _ticker;
        private decimal? _venda;
        private decimal _estoqueAcumulado,
                        _estoqueValor,
                        _custo,
                        _lucroPrejuizo,
                        _lucroPrejuizoMensal,
                        _lucroPrejuizoAcumulado,
                        _lucroEmReal;

        #endregion

        #region Construtor

        public OperacaoCambioDTO() { }

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
        public string Dd_Mm_Yyyy_Operacao
        {
            get
            {
                if (DataOperacao.Length == 10)
                    return DataOperacao.Substring(8, 2) + "/" +
                           DataOperacao.Substring(5, 2) + "/" +
                           DataOperacao.Substring(0, 4);
                else
                    return "";
            }
        }
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
        public decimal ValorTotal
        {
            get => _valorTotal;
            set
            {
                if (_valorTotal != value)
                {
                    _valorTotal = value;
                    OnPropertyChanged(nameof(ValorTotal));
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
        public decimal? Venda
        {
            get => _venda;
            set
            {
                if (_venda != value)
                {
                    _venda = value;
                    OnPropertyChanged(nameof(Venda));
                }
            }
        }

        public decimal EstoqueAcumulado
        {
            get => _estoqueAcumulado;
            set
            {
                if (_estoqueAcumulado != value)
                {
                    _estoqueAcumulado = value;
                    OnPropertyChanged(nameof(EstoqueAcumulado));
                }
            }
        }
        public decimal EstoqueValor
        {
            get => _estoqueValor;
            set
            {
                if (_estoqueValor != value)
                {
                    _estoqueValor = value;
                    OnPropertyChanged(nameof(EstoqueValor));
                }
            }
        }
        public decimal Custo
        {
            get => _custo;
            set
            {
                if (_custo != value)
                {
                    _custo = value;
                    OnPropertyChanged(nameof(Custo));
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

        public decimal LucroPrejuizoMensal
        {
            get => _lucroPrejuizoMensal;
            set
            {
                if (_lucroPrejuizoMensal != value)
                {
                    _lucroPrejuizoMensal = value;
                    OnPropertyChanged(nameof(LucroPrejuizoMensal));
                }
            }
        }
        public decimal LucroPrejuizoAcumulado
        {
            get => _lucroPrejuizoAcumulado;
            set
            {
                if (_lucroPrejuizoAcumulado != value)
                {
                    _lucroPrejuizoAcumulado = value;
                    OnPropertyChanged(nameof(LucroPrejuizoAcumulado));
                }
            }
        }

        public decimal LucroEmReal
        {
            get => _lucroEmReal;
            set
            {
                if (_lucroEmReal != value)
                {
                    _lucroEmReal = value;
                    OnPropertyChanged(nameof(LucroEmReal));
                }
            }
        }

        public string EstoqueAcumuladoStr
        {
            get => ((_estoqueAcumulado > 0) ? _estoqueAcumulado.ToString("#,##0.####0") : "");
        }
        public string EstoqueValorStr
        {
            get => ((EstoqueValor > 0) ? EstoqueValor.ToString("#,##0.#0") : "");
        }
        public string CustoStr
        {
            get => ((Custo != 0) ? Custo.ToString("#,##0.#0") : "");
        }
        public string LucroPrejuizoStr
        {
            get => ((LucroPrejuizo != 0) ? LucroPrejuizo.ToString("#,##0.#0") : "");
        }
        public string LucroPrejuizoMensalStr
        {
            get => ((_lucroPrejuizoMensal != 0) ? _lucroPrejuizoMensal.ToString("#,##0.#0") : "");
        }
        public string LucroPrejuizoAcumuladoStr
        {
            get => ((_lucroPrejuizoAcumulado != 0) ? _lucroPrejuizoAcumulado.ToString("#,##0.#0") : "");
        }
        public string LucroEmRealStr
        {
            get => ((_lucroEmReal > 0) ? _lucroEmReal.ToString("#,##0.#0") : "");
        }

        #endregion
    }
}