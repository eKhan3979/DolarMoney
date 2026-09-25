using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class IFRViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private decimal _alfa;

        private int _mmDias;

        private IdDescricaoDTO _acaoSelecionada = new IdDescricaoDTO();

        private DateTime _periodoDe,
                         _periodoAte;

        private ObservableCollection<IdDescricaoDTO> _listaAcoes = new ObservableCollection<IdDescricaoDTO>();
        private ObservableCollection<CotacaoGraficoDTO> _listaCotacoes = new ObservableCollection<CotacaoGraficoDTO>();
        private List<int> _listaDiasMM = new List<int>();
        private List<CotacaoGraficoDTO> _listaMAltas = new List<CotacaoGraficoDTO>();
        private List<CotacaoGraficoDTO> _listaMBaixas = new List<CotacaoGraficoDTO>();
        private List<CotacaoIFRDTO> _listaIFR = new List<CotacaoIFRDTO>();

        #endregion

        #region Construtor

        public IFRViewModel() { }

        #endregion

        #region Propriedades

        public IdDescricaoDTO AcaoSelecionada
        {
            get => _acaoSelecionada;
            set
            {
                if (_acaoSelecionada != value)
                {
                    _acaoSelecionada = value;
                    OnPropertyChanged(nameof(AcaoSelecionada));
                }
            }
        }

        public decimal Alfa
        {
            get => _alfa;
            set
            {
                if (_alfa != value)
                {
                    _alfa = value;
                    OnPropertyChanged(nameof(Alfa));
                }
            }
        }

        public int MMDias
        {
            get => _mmDias;
            set
            {
                if (_mmDias != value)
                {
                    _mmDias = value;
                    OnPropertyChanged(nameof(MMDias));
                }
            }
        }

        public DateTime PeriodoDe
        {
            get => _periodoDe;
            set
            {
                if (_periodoDe != value)
                {
                    _periodoDe = value;
                    OnPropertyChanged(nameof(PeriodoDe));
                }
            }
        }
        public DateTime PeriodoAte
        {
            get => _periodoAte;
            set
            {
                if (_periodoAte != value)
                {
                    _periodoAte = value;
                    OnPropertyChanged(nameof(PeriodoAte));
                }
            }
        }

        public ObservableCollection<IdDescricaoDTO> ListaAcoes
        {
            get => _listaAcoes;
            set
            {
                if (_listaAcoes != value)
                {
                    _listaAcoes = value;
                    OnPropertyChanged(nameof(ListaAcoes));
                }
            }
        }
        public ObservableCollection<CotacaoGraficoDTO> ListaCotacoes
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
        public List<int> ListaDiasMM
        {
            get => _listaDiasMM;
            set
            {
                if (_listaDiasMM != value)
                {
                    _listaDiasMM = value;
                    OnPropertyChanged(nameof(ListaDiasMM));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaMAltas
        {
            get => _listaMAltas;
            set
            {
                if (_listaMAltas != value)
                {
                    _listaMAltas = value;
                    OnPropertyChanged(nameof(ListaMAltas));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaMBaixas
        {
            get => _listaMBaixas;
            set
            {
                if (_listaMBaixas != value)
                {
                    _listaMBaixas = value;
                    OnPropertyChanged(nameof(ListaMBaixas));
                }
            }
        }
        public List<CotacaoIFRDTO> ListaIFR
        {
            get => _listaIFR;
            set
            {
                if (_listaIFR != value)
                {
                    _listaIFR = value;
                    OnPropertyChanged(nameof(ListaIFR));
                }
            }
        }

        #endregion
    }
}