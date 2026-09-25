namespace DolarMoney.Model.DTO
{
    public class InvestimentoDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _idInvestimento,
                    _idTipoInvestimento;
        private string _ticker = "",
                       _nome = "";
        private DateTime _dataCadastro;
        private bool _ativo;

        private int _numero;
        private string _ativoSN,
                       _dd_Mm_Yyyy_Cadastro;

        #endregion

        #region Construtor

        public InvestimentoDTO(InvestimentoModel investimento)
        {
            IdInvestimento = investimento.IdInvestimento;
            IdTipoInvestimento = investimento.IdTipoInvestimento;
            Ticker = investimento.Ticker;
            Nome = investimento.Nome;
            DataCadastro = investimento.DataCadastro;
            Ativo = investimento.Ativo;
        }

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

                    Dd_Mm_Yyyy_Cadastro = _dataCadastro.ToString("dd/MM/yyyy");
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

                    AtivoSN = ((_ativo) ? "Sim" : "Não");
                }
            }
        }

        public string AtivoSN
        {
            get => _ativoSN;
            set
            {
                if (_ativoSN != value)
                {
                    _ativoSN = value;
                    OnPropertyChanged(nameof(AtivoSN));
                }
            }
        }
        public string Dd_Mm_Yyyy_Cadastro
        {
            get => _dd_Mm_Yyyy_Cadastro;
            set
            {
                if (_dd_Mm_Yyyy_Cadastro != value)
                {
                    _dd_Mm_Yyyy_Cadastro = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy_Cadastro));
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

        #endregion
    }
}
