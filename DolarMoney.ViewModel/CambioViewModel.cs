using System;
using System.Collections.Generic;

using DolarMoney.Model;

namespace DolarMoney.ViewModel
{
    public class CambioViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private string _pathArquivoTexto = "";
        
        private List<CambioModel> _listaCambio;

        #endregion

        #region Construtor

        public CambioViewModel() { }

        #endregion

        #region Propriedades

        public string PathArquivoTexto
        {
            get => _pathArquivoTexto;
            set
            {
                if (_pathArquivoTexto != value)
                {
                    _pathArquivoTexto = value;
                    OnPropertyChanged(nameof(PathArquivoTexto));
                }
            }
        }

        public List<CambioModel> ListaCambio
        {
            get => _listaCambio;
            set
            {
                if (_listaCambio != value)
                {
                    _listaCambio = value;
                    OnPropertyChanged(nameof(ListaCambio));
                }
            }
        }

        #endregion
    }
}