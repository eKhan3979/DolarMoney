namespace DolarMoney.Model
{
    public class InvestimentoModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idInvestimento,
                    _idTipoInvestimento;
        private string _ticker,
                       _nome;
        private DateTime _dataCadastro;
        private bool _ativo;

        #endregion

        #region Construtor

        public InvestimentoModel() { }

        #endregion

        #region Propriedades

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
        public int IdTipoInvestimento
        {
            get => _idTipoInvestimento;
            set
            {
                if (_idTipoInvestimento != value)
                {
                    _idTipoInvestimento = value;
                    OnPropertyChanged(nameof(IdTipoInvestimento));
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
        public DateTime DataCadastro
        {
            get => _dataCadastro;
            set
            {
                if (_dataCadastro != value)
                {
                    _dataCadastro = value;
                    OnPropertyChanged(nameof(DataCadastro));
                }
            }
        }
        public bool Ativo
        {
            get => _ativo;
            set
            {
                if (_ativo != value)
                {
                    _ativo = value;
                    OnPropertyChanged(nameof(Ativo));
                }
            }
        }

        #endregion
    }
}