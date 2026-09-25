using System;
using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class OperacaoViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private EstoqueDTO _investimentoSelecionado;
        private OperacaoPesquisarDTO _pesquisar;
        private OperacaoModel _cadastro;
        private ResumoDTO _resumoPesquisa;

        private ObservableCollection<CodigoDescricaoDTO> _listaCadCV = new ObservableCollection<CodigoDescricaoDTO>();
        private ObservableCollection<EstoqueDTO> _listaEstoques = new ObservableCollection<EstoqueDTO>();
        private ObservableCollection<InvestimentoModel> _listaInvestimentos = new ObservableCollection<InvestimentoModel>();
        private ObservableCollection<InvestimentoModel> _listaInvestimentosExibir = new ObservableCollection<InvestimentoModel>();
        private ObservableCollection<OperacaoInvestimentoDTO> _listaOperacoesInvestimentos = new ObservableCollection<OperacaoInvestimentoDTO>();
        private ObservableCollection<OperacaoModel> _listaOperacoes = new ObservableCollection<OperacaoModel>();
        private ObservableCollection<TipoInvestimentoModel> _listaTipoInvestimento = new ObservableCollection<TipoInvestimentoModel>();

        #endregion

        #region Construtor

        public OperacaoViewModel() { }

        #endregion

        #region Propriedades

        public OperacaoModel Cadastro
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

        public EstoqueDTO InvestimentoSelecionado
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

        public ObservableCollection<CodigoDescricaoDTO> ListaCadCV
        {
            get => _listaCadCV;
            set
            {
                if (_listaCadCV != value)
                {
                    _listaCadCV = value;
                    OnPropertyChanged(nameof(ListaCadCV));
                }
            }
        }

        public ObservableCollection<EstoqueDTO> ListaEstoques
        {
            get => _listaEstoques;
            set
            {
                if (_listaEstoques != value)
                {
                    _listaEstoques = value;
                    OnPropertyChanged(nameof(ListaEstoques));
                }
            }
        }

        public ObservableCollection<InvestimentoModel> ListaInvestimentos
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

        public ObservableCollection<InvestimentoModel> ListaInvestimentosExibir
        {
            get => _listaInvestimentosExibir;
            set
            {
                if (_listaInvestimentosExibir != value)
                {
                    _listaInvestimentosExibir = value;
                    OnPropertyChanged(nameof(ListaInvestimentosExibir));
                }
            }
        }

        public ObservableCollection<OperacaoModel> ListaOperacoes
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

        public ObservableCollection<OperacaoInvestimentoDTO> ListaOperacoesInvestimentos
        {
            get => _listaOperacoesInvestimentos;
            set
            {
                if (_listaOperacoesInvestimentos != value)
                {
                    _listaOperacoesInvestimentos = value;
                    OnPropertyChanged(nameof(ListaOperacoesInvestimentos));
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

        public OperacaoPesquisarDTO Pesquisar
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

        public ResumoDTO ResumoPesquisa
        {
            get => _resumoPesquisa;
            set
            {
                if (_resumoPesquisa != value)
                {
                    _resumoPesquisa = value;
                    OnPropertyChanged(nameof(ResumoPesquisa));
                }
            }
        }

        #endregion
    }
}