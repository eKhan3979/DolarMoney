using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class ImportVolumesViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private IdDescricaoDTO _investimentoSelecionado;
        private string _pathCSV,
                       _pathXLSX;

        private ObservableCollection<IdDescricaoDTO> _listaInvestimentos;
        private List<CotacaoVolumeDTO> _listaVolumes;

        #endregion

        #region Construtor

        public ImportVolumesViewModel() { }

        #endregion

        #region Propriedades

        public IdDescricaoDTO InvestimentoSelecionado
        {
            get => _investimentoSelecionado;
            set
            {
                if (_investimentoSelecionado != value)
                {
                    _investimentoSelecionado = value;
                    OnPropertyChanged(nameof(InvestimentoSelecionado));
                }
            }
        }

        public string PathCSV
        {
            get => _pathCSV;
            set
            {
                if (_pathCSV != value)
                {
                    _pathCSV = value;
                    OnPropertyChanged(nameof(PathCSV));
                }
            }
        }

        public string PathXLSX
        {
            get => _pathXLSX;
            set
            {
                if (_pathXLSX != value)
                {
                    _pathXLSX = value;
                    OnPropertyChanged(nameof(PathXLSX));
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

        public List<CotacaoVolumeDTO> ListaVolumes
        {
            get => _listaVolumes;
            set
            {
                if (_listaVolumes != value)
                {
                    _listaVolumes = value;
                    OnPropertyChanged(nameof(ListaVolumes));
                }
            }
        }

        #endregion
    }
}