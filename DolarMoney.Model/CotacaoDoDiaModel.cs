using System.Diagnostics.Contracts;

namespace DolarMoney.Model
{
    public class CotacaoDoDiaModel: BaseModel
    {
        #region Variáveis da Classe

        private int _idInvestimento;
        private bool _ativo;

        #endregion

        #region Construtor

        public CotacaoDoDiaModel() { }

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