namespace DolarMoney.Model.DTO
{
    public class IdDescricaoDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _id;
        private string _descricao;

        #endregion

        #region Construtor

        public IdDescricaoDTO() { }

        #endregion

        #region Propriedades

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
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

        #endregion
    }
}