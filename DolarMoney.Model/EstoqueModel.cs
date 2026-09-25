namespace DolarMoney.Model
{
    public class EstoqueModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idEstoque,
                    _idInvestimento;
        private string _dataOperacao,
                       _indice;
        private decimal _quantidade,
                        _valorTotal,
                        _lucroPrejuizo;

        #endregion

        #region Construtor

        public EstoqueModel() { }

        #endregion

        #region Propriedades

        public int IdEstoque
        {
            get => _idEstoque;
            set
            {
                if (_idEstoque != value)
                {
                    _idEstoque = value;
                    OnPropertyChanged(nameof(IdEstoque));
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

        #endregion
    }
}