namespace DolarMoney.Model
{
    public class TipoInvestimentoModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idTipoInvestimento;
	    private string _tipo;
        private bool _ativo;

        #endregion

        #region Construtor

        public TipoInvestimentoModel() { }

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
        public string Tipo
        {
            get => _tipo;
            set
            {
                if (_tipo != value)
                {
                    _tipo = value;
                    OnPropertyChanged(nameof(Tipo));
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