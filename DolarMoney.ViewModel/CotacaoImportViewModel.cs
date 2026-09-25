using System;
using System.Collections.ObjectModel;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.ViewModel
{
    public class CotacaoImportViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private int _idInvestimentoImport,
                    _registrosAGravar,
                    _registrosGravados;
        private string _pathImport;

        private ObservableCollection<CotacaoImportDTO> _listaCotacoes;
        private ObservableCollection<InvestimentoModel> _listaInvestimentos;

        #endregion

        #region Construtor

        public CotacaoImportViewModel() { }

        #endregion

        #region Propriedades

        public int IdInvestimentoImport
        {
            get => _idInvestimentoImport;
            set
            {
                if (_idInvestimentoImport != value)
                {
                    _idInvestimentoImport = value;
                    OnPropertyChanged(nameof(IdInvestimentoImport));
                }
            }
        }
        public string PathImport
        {
            get => _pathImport;
            set
            {
                if (_pathImport != value)
                {
                    _pathImport = value;
                    OnPropertyChanged(nameof(PathImport));
                }
            }
        }
        public int RegistrosAGravar
        {
            get => _registrosAGravar;
            set
            {
                if (_registrosAGravar != value)
                {
                    _registrosAGravar = value;
                    OnPropertyChanged(nameof(RegistrosAGravar));
                }
            }
        }
        public int RegistrosGravados
        {
            get => _registrosGravados;
            set
            {
                if (_registrosGravados != value)
                {
                    _registrosGravados = value;
                    OnPropertyChanged(nameof(RegistrosGravados));
                }
            }
        }

        public ObservableCollection<CotacaoImportDTO> ListaCotacoes
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

        #endregion
    }
}