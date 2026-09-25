using System;
using System.Collections.ObjectModel;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class GraficosViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private int _mmDias1,
                    _mmDias2;   

        private IdDescricaoDTO _acaoSelecionada = new IdDescricaoDTO();

        private AnaliseModel _analiseAtual = new AnaliseModel();

        private DateTime _periodoDe,
                         _periodoAte;

        private ObservableCollection<IdDescricaoDTO> _listaAcoes = new ObservableCollection<IdDescricaoDTO>();
        private ObservableCollection<CotacaoGraficoDTO> _listaCotacoes = new ObservableCollection<CotacaoGraficoDTO>();
        private ObservableCollection<int> _listaDiasMM1 = new ObservableCollection<int>();
        private ObservableCollection<int> _listaDiasMM2 = new ObservableCollection<int>();
        private ObservableCollection<IdDescricaoDTO> _listaExpirado = new ObservableCollection<IdDescricaoDTO>();
        private ObservableCollection<CotacaoGraficoDTO> _listaMM1 = new ObservableCollection<CotacaoGraficoDTO>();
        private ObservableCollection<CotacaoGraficoDTO> _listaMM2 = new ObservableCollection<CotacaoGraficoDTO>();
        private ObservableCollection<TendenciaModel> _listaTendencia = new ObservableCollection<TendenciaModel>();
        private ObservableCollection<TendenciaAtualDTO> _listaTendenciasAtuais = new ObservableCollection<TendenciaAtualDTO>();

        #endregion

        #region Construtor

        public GraficosViewModel() { }

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
        public ObservableCollection<int> ListaDiasMM1
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
        public ObservableCollection<int> ListaDiasMM2
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
        public ObservableCollection<IdDescricaoDTO> ListaExpirado
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
        public ObservableCollection<CotacaoGraficoDTO> ListaMM1
        {
            get => _listaMM1;
            set
            {
                if (_listaMM1 != value)
                {
                    _listaMM1 = value;
                    OnPropertyChanged(nameof(ListaMM1));
                }
            }
        }
        public ObservableCollection<CotacaoGraficoDTO> ListaMM2
        {
            get => _listaMM2;
            set
            {
                if (_listaMM2 != value)
                {
                    _listaMM2 = value;
                    OnPropertyChanged(nameof(ListaMM2));
                }
            }
        }
        public ObservableCollection<TendenciaModel> ListaTendencia
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