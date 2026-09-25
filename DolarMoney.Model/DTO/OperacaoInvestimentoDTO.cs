namespace DolarMoney.Model.DTO
{
    public class OperacaoInvestimentoDTO : OperacaoModel
    {
        #region Variáveis da Classe

        private decimal _custoEstoque,
                        _qtdeEstoque,
                        _valorAtual,
                        _valorizacao;
        private string _custoEstoqueStr,
                       _qtdeEstoqueStr,
                       _valorAtualStr,
                       _valorizacaoStr;

        #endregion

        #region Construtor

        public OperacaoInvestimentoDTO() { }

        #endregion

        #region Propriedades

        public decimal CustoEstoque
        {
            get => _custoEstoque;
            set
            {
                //if (_custoEstoque != value)
                {
                    _custoEstoque = value;
                    OnPropertyChanged(nameof(CustoEstoque));

                    if (_custoEstoque < 0.01m)
                        CustoEstoqueStr = "-"; 
                    else
                        CustoEstoqueStr = _custoEstoque.ToString("#,##0.#0");
                }
            }
        }

        public string CustoEstoqueStr
        {
            get => _custoEstoque.ToString("#,##0.#0");
            set
            {
                //if (_custoEstoqueStr != value)
                {
                    _custoEstoqueStr = value;
                    OnPropertyChanged(nameof(CustoEstoqueStr));
                }
            }
        }

        public string LucroPrejuizoStr
        {
            get => ((LucroPrejuizo != 0) ? LucroPrejuizo.ToString("#,##0.#0") : "-");
        }

        public decimal QtdeEstoque
        {
            get => _qtdeEstoque;
            set
            {
                //if (_qtdeEstoque != value)
                {
                    _qtdeEstoque = value;
                    OnPropertyChanged(nameof(QtdeEstoque));

                    if (_qtdeEstoque > 0)
                        QtdeEstoqueStr = _qtdeEstoque.ToString("#.####0");
                    else
                        QtdeEstoqueStr = "-";
                }
            }
        }

        public string QtdeEstoqueStr
        {
            get => _qtdeEstoqueStr;
            set
            {
                //if (_qtdeEstoqueStr != value)
                {
                    _qtdeEstoqueStr = value;
                    OnPropertyChanged(nameof(QtdeEstoqueStr));
                }
            }
        }

        public decimal ValorAtual
        {
            get => _valorAtual;
            set
            {
                //if (_valorAtual != value)
                {
                    _valorAtual = value;
                    OnPropertyChanged(nameof(ValorAtual));

                    if (_valorAtual > 0)
                        ValorAtualStr = _valorAtual.ToString("#,##0.#0");
                    else
                        ValorAtualStr = "-";
                }
            }
        }

        public string ValorAtualStr
        {
            get => _valorAtualStr;
            set
            {
                //if (_valorAtualStr != value)
                {
                    _valorAtualStr = value;
                    OnPropertyChanged(nameof(ValorAtualStr));
                }
            }
        }

        public decimal Valorizacao
        {
            get => _valorizacao;
            set
            {
                if (_valorizacao != value)
                {
                    _valorizacao = value;
                    OnPropertyChanged(nameof(Valorizacao));

                    if (_valorizacao != 0)
                        ValorizacaoStr = _valorizacao.ToString("#,##0.#0");
                    else
                        ValorizacaoStr = "-";
                }
            }
        }

        public string ValorizacaoStr
        {
            get => _valorizacaoStr;
            set
            {
                if (_valorizacaoStr != value)
                {
                    _valorizacaoStr = value;
                    OnPropertyChanged(nameof(ValorizacaoStr));
                }
            }
        }

        #endregion
    }
}