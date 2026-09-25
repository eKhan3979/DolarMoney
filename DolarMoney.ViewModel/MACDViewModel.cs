using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class MACDViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private decimal _alfa;

        private int _mmDias1,
                    _mmDias2;

        private IdDescricaoDTO _acaoSelecionada = new IdDescricaoDTO();

        private AnaliseModel _analiseAtual = new AnaliseModel();

        private DateTime _periodoDe,
                         _periodoAte;

        private ObservableCollection<IdDescricaoDTO> _listaAcoes = new ObservableCollection<IdDescricaoDTO>();
        private ObservableCollection<CotacaoGraficoDTO> _listaCotacoes = new ObservableCollection<CotacaoGraficoDTO>();
        private List<int> _listaDiasMM1 = new List<int>();
        private List<int> _listaDiasMM2 = new List<int>();
        private List<CotacaoGraficoDTO> _listaEMA1 = new List<CotacaoGraficoDTO>();
        private List<CotacaoGraficoDTO> _listaEMA2 = new List<CotacaoGraficoDTO>();
        private List<IdDescricaoDTO> _listaExpirado = new List<IdDescricaoDTO>();
        private List<CotacaoGraficoDTO> _listaMedia1 = new List<CotacaoGraficoDTO>();
        private List<CotacaoMacdDTO> _listaMACD = new List<CotacaoMacdDTO>();
        private List<CotacaoGraficoDTO> _listaMedia2 = new List<CotacaoGraficoDTO>();
        private List<TendenciaModel> _listaTendencia = new List<TendenciaModel>();
        private ObservableCollection<TendenciaAtualDTO> _listaTendenciasAtuais = new ObservableCollection<TendenciaAtualDTO>();

        #endregion

        #region Construtor

        public MACDViewModel() { }

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

        public AnaliseModel AnaliseAtual
        {
            get => _analiseAtual;
            set
            {
                if (_analiseAtual != value)
                {
                    _analiseAtual = value;
                    OnPropertyChanged(nameof(AnaliseAtual));
                }
            }
        }

        public int MMDias1
        {
            get => _mmDias1;
            set
            {
                if (_mmDias1 != value)
                {
                    _mmDias1 = value;
                    OnPropertyChanged(nameof(MMDias1));
                }
            }
        }
        public int MMDias2
        {
            get => _mmDias2;
            set
            {
                if (_mmDias2 != value)
                {
                    _mmDias2 = value;
                    OnPropertyChanged(nameof(MMDias2));
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
        public List<int> ListaDiasMM1
        {
            get => _listaDiasMM1;
            set
            {
                if (_listaDiasMM1 != value)
                {
                    _listaDiasMM1 = value;
                    OnPropertyChanged(nameof(ListaDiasMM1));
                }
            }
        }
        public List<int> ListaDiasMM2
        {
            get => _listaDiasMM2;
            set
            {
                if (_listaDiasMM2 != value)
                {
                    _listaDiasMM2 = value;
                    OnPropertyChanged(nameof(ListaDiasMM2));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaEMA1
        {
            get => _listaEMA1;
            set
            {
                if (_listaEMA1 != value)
                {
                    _listaEMA1 = value;
                    OnPropertyChanged(nameof(ListaEMA1));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaEMA2
        {
            get => _listaEMA2;
            set
            {
                if (_listaEMA2 != value)
                {
                    _listaEMA2 = value;
                    OnPropertyChanged(nameof(ListaEMA2));
                }
            }
        }
        public List<IdDescricaoDTO> ListaExpirado
        {
            get => _listaExpirado;
            set
            {
                if (_listaExpirado != value)
                {
                    _listaExpirado = value;
                    OnPropertyChanged(nameof(ListaExpirado));
                }
            }
        }
        public List<CotacaoMacdDTO> ListaMACD
        {
            get => _listaMACD;
            set
            {
                if (_listaMACD != value)
                {
                    _listaMACD = value;
                    OnPropertyChanged(nameof(ListaMACD));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaMedia1
        {
            get => _listaMedia1;
            set
            {
                if (_listaMedia1 != value)
                {
                    _listaMedia1 = value;
                    OnPropertyChanged(nameof(ListaMedia1));
                }
            }
        }
        public List<CotacaoGraficoDTO> ListaMedia2
        {
            get => _listaMedia2;
            set
            {
                if (_listaMedia2 != value)
                {
                    _listaMedia2 = value;
                    OnPropertyChanged(nameof(ListaMedia2));
                }
            }
        }
        public List<TendenciaModel> ListaTendencia
        {
            get => _listaTendencia;
            set
            {
                if (_listaTendencia != value)
                {
                    _listaTendencia = value;
                    OnPropertyChanged(nameof(ListaTendencia));
                }
            }
        }
        public ObservableCollection<TendenciaAtualDTO> ListaTendenciasAtuais
        {
            get => _listaTendenciasAtuais;
            set
            {
                if (_listaTendenciasAtuais != value)
                {
                    _listaTendenciasAtuais = value;
                    OnPropertyChanged(nameof(ListaTendenciasAtuais));
                }
            }
        }

        #endregion
    }
}