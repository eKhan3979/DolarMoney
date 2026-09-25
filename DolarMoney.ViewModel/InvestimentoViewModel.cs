using System;
using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class InvestimentoViewModel : BaseViewModel
    {
        #region Variáveis da Classe

        private InvestimentoModel _investimentoEdit;
        
        private ObservableCollection<CodigoDescricaoDTO> _listaAtivo;
        private ObservableCollection<InvestimentoDTO> _listaInvestimentos;
        private ObservableCollection<TipoInvestimentoModel> _listaTipoInvestimento;

        #endregion

        #region Construtor

        public InvestimentoViewModel() { }

        #endregion

        #region Propriedades

        public InvestimentoModel InvestimentoEdit
        {
            get => _investimentoEdit;
            set
            {
                if (_investimentoEdit != value)
                {
                    _investimentoEdit = value;
                    OnPropertyChanged(nameof(InvestimentoEdit));
                }
            }
        }

        public ObservableCollection<CodigoDescricaoDTO> ListaAtivo
        {
            get => _listaAtivo;
            set
            {
                if (_listaAtivo != value)
                {
                    _listaAtivo = value;
                    OnPropertyChanged(nameof(ListaAtivo));
                }
            }
        }
        public ObservableCollection<InvestimentoDTO> ListaInvestimentos
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
        public ObservableCollection<TipoInvestimentoModel> ListaTipoInvestimento
        {
            get => _listaTipoInvestimento;
            set
            {
                if (_listaTipoInvestimento != value)
                {
                    _listaTipoInvestimento = value;
                    OnPropertyChanged(nameof(ListaTipoInvestimento));
                }
            }
        }

        #endregion
    }
}