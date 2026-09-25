using DolarMoney.Model;
using DolarMoney.Model.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DolarMoney.ViewModel
{
    public class IRViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private DateTime _dataInicial,
                         _dataFinal;

        private List<OperacaoCambioDTO> _listaOperacoes = new List<OperacaoCambioDTO>();

        #endregion

        #region Construtor

        public IRViewModel() { }

        #endregion

        #region Propriedades

        public DateTime DataInicial
        {
            get => _dataInicial;
            set
            {
                if (_dataInicial != value)
                {
                    _dataInicial = value;
                    OnPropertyChanged(nameof(DataInicial));
                }
            }
        }

        public DateTime DataFinal
        {
            get => _dataFinal;
            set
            {
                if (_dataFinal != value)
                {
                    _dataFinal = value;
                    OnPropertyChanged(nameof(DataFinal));
                }
            }
        }

        public List<OperacaoCambioDTO> ListaOperacoes
        {
            get => _listaOperacoes;
            set
            {
                if (_listaOperacoes != value)
                {
                    _listaOperacoes = value;
                    OnPropertyChanged(nameof(ListaOperacoes));
                }
            }
        }

        #endregion
    }
}