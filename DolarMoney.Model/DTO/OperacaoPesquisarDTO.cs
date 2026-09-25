namespace DolarMoney.Model.DTO
{
    public class OperacaoPesquisarDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _idTipoInvestimento,
                    _idInvestimento;

        private string _tipoInvestimento;

        private DateTime _periodoDe,
                         _periodoAte;

        #endregion

        #region Construtor

        public OperacaoPesquisarDTO() { }

        #endregion

        #region Propriedades

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

        public string TipoInvestimento
        {
            get => _tipoInvestimento;
            set
            {
                if (_tipoInvestimento != value)
                {
                    _tipoInvestimento = value;
                    OnPropertyChanged(nameof(TipoInvestimento));
                }
            }
        }

        public DateTime PeriodoDe
        {
            get => _periodoDe;
            set
            {
                if (_periodoDe != value)
                {
                    _periodoDe = value;
                    OnPropertyChanged(nameof(PeriodoDe));
                }
            }
        }
        public DateTime PeriodoAte
        {
            get => _periodoAte;
            set
            {
                if (_periodoAte != value)
                {
                    _periodoAte = value;
                    OnPropertyChanged(nameof(PeriodoAte));
                }
            }
        }

        #endregion
    }
}