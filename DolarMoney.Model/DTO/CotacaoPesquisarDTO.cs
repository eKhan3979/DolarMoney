namespace DolarMoney.Model.DTO
{
    public class CotacaoPesquisarDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _idInvestimentoSelecionado;
        private DateTime _periodoDe,
                         _periodoAte;

        #endregion

        #region Construtor

        public CotacaoPesquisarDTO() { }

        #endregion

        #region Propriedades

        public int IdInvestimentoSelecionado
        {
            get => _idInvestimentoSelecionado;
            set
            {
                if (_idInvestimentoSelecionado != value)
                {
                    _idInvestimentoSelecionado = value;
                    OnPropertyChanged(nameof(IdInvestimentoSelecionado));
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