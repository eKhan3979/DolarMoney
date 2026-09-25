namespace DolarMoney.Model.DTO
{
    public class CodigoDescricaoDTO: BaseModel
    {
        #region Variáveis da Classe

        private string _codigo,
                       _descricao;

        private bool _check;

        #endregion

        #region Construtor

        public CodigoDescricaoDTO() { }

        #endregion

        #region Propriedades

        public string Codigo
        {
            get => _codigo;
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    OnPropertyChanged(nameof(Codigo));
                }
            }
        }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (_descricao != value)
                {
                    _descricao = value;
                    OnPropertyChanged(nameof(Descricao));
                }
            }
        }
        public bool Check
        {
            get => _check;
            set
            {
                if (_check != value)
                {
                    _check = value;
                    OnPropertyChanged(nameof(Check));
                }
            }
        }

        #endregion
    }
}