using System;

using DolarMoney.Model;

namespace DolarMoney.ViewModel
{
    public class InvestingViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private string _pathHtml;
        private DateTime _dataImportacao;

        private List<CotacaoInvestingModel> _listaCotacaoes;

        #endregion

        #region Construtor

        public InvestingViewModel() { }

        #endregion

        #region Propriedades

        public string PathHTML
        {
            get => _pathHtml;
            set
            {
                if (_pathHtml != value)
                {
                    _pathHtml = value;
                    OnPropertyChanged(nameof(PathHTML));
                }
            }
        }

        public DateTime DataImportacao
        {
            get => _dataImportacao;
            set
            {
                if (_dataImportacao != value)
                {
                    _dataImportacao = value;
                    OnPropertyChanged(nameof(DataImportacao));
                }
            }
        }

        public List<CotacaoInvestingModel> ListaCotacaoes
        {
            get => _listaCotacaoes;
            set
            {
                if (_listaCotacaoes != value)
                {
                    _listaCotacaoes = value;
                    OnPropertyChanged(nameof(ListaCotacaoes));
                }
            }
        }

        #endregion
    }
}