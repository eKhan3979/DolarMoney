using System;
using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class CotacaoViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private CotacaoPesquisarDTO _pesquisar;
        private CotacaoModel _cadastro;

        private ObservableCollection<CotacaoModel> _listaCotacoes;
        private ObservableCollection<IdDescricaoDTO> _listaInvestimentos;

        #endregion

        #region Construtor

        public CotacaoViewModel() { }

        #endregion

        #region Propriedades

        public CotacaoPesquisarDTO Pesquisar
        {
            get => _pesquisar;
            set
            {
                if (_pesquisar != value)
                {
                    _pesquisar = value;
                    OnPropertyChanged(nameof(Pesquisar));
                }
            }
        }
        public CotacaoModel Cadastro
        {
            get => _cadastro;
            set
            {
                if (_cadastro != value)
                {
                    _cadastro = value;
                    OnPropertyChanged(nameof(Cadastro));
                }
            }
        }
 
        public ObservableCollection<CotacaoModel> ListaCotacoes
        {
            get => _listaCotacoes;
            set
            {
                if (_listaCotacoes != value)
                {
                    _listaCotacoes = value;
                    OnPropertyChanged(nameof(ListaCotacoes));
                }
            }
        }
        public ObservableCollection<IdDescricaoDTO> ListaInvestimentos
        {
            get => _listaInvestimentos;
            set
            {
                if (_listaInvestimentos != value)
                {
                    _listaInvestimentos = value;
                    OnPropertyChanged(nameof(ListaInvestimentos));
                }
            }
        }

        #endregion
    }
}