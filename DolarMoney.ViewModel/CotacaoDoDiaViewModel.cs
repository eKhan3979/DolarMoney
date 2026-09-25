using System;
using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class CotacaoDoDiaViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private DateTime _dataCotacao = DateTime.Today;
        private DateTime? _dataPesquisa;
        private CotacaoModel _cotacaoEdit;
        private List<CotacaoPercentualDTO> _listaCotacoes = new List<CotacaoPercentualDTO>();
        private List<CotacaoDoDiaDTO> _listaCotacaoDoDia = new List<CotacaoDoDiaDTO>();
        //private ObservableCollection<CotacaoDoDiaDTO> _listaCotacaoDoDia = new ObservableCollection<CotacaoDoDiaDTO>();
        private ObservableCollection<IdDescricaoDTO> _listaInvestimentos = new ObservableCollection<IdDescricaoDTO>();

        #endregion

        #region Construtor

        public CotacaoDoDiaViewModel() { }

        #endregion

        #region Propriedades

        public CotacaoModel CotacaoEdit
        {
            get => _cotacaoEdit;
            set
            {
                if (_cotacaoEdit != value)
                {
                    _cotacaoEdit = value;
                    OnPropertyChanged(nameof(CotacaoEdit));
                }
            }
        }

        public DateTime DataCotacao
        {
            get => _dataCotacao;
            set
            {
                if (_dataCotacao != value)
                {
                    _dataCotacao = value;
                    OnPropertyChanged(nameof(DataCotacao));
                }
            }
        }

        public DateTime? DataPesquisa
        {
            get => _dataPesquisa;
            set
            {
                if (_dataPesquisa != value)
                {
                    _dataPesquisa = value;
                    OnPropertyChanged(nameof(DataPesquisa));
                }
            }
        }

        public List<CotacaoPercentualDTO> ListaCotacoes
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

        public List<CotacaoDoDiaDTO> ListaCotacaoDoDia
        {
            get => _listaCotacaoDoDia;
            set
            {
                if (_listaCotacaoDoDia != value)
                {
                    _listaCotacaoDoDia = value;
                    OnPropertyChanged(nameof(ListaCotacaoDoDia));
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