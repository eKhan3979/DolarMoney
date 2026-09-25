using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles
{
    public partial class UcrCotacoes : UserControl
    {
        #region Variáveis da Classe

        private CotacaoRegras _Regras;
        private CotacaoViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrCotacoes()
        {
            InitializeComponent();

            if ((_ViewModel == null) ||
                (!_ViewModel.Iniciado))
            {
                _ViewModel = new CotacaoViewModel();
                _Regras = new CotacaoRegras(ref _ViewModel);

                DataContext = _ViewModel;

                ThreadStart tstInicializar = new ThreadStart(Inicializar);
                Thread thrInicializar = new Thread(tstInicializar);
                thrInicializar.IsBackground = true;
                thrInicializar.Start();
            }
        }

        private async void Inicializar()
        {
            try
            {
                _ViewModel.Iniciado = await _Regras.Iniciar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            _Regras.Limpar();
        }

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            if (_ViewModel.Pesquisar.IdInvestimentoSelecionado > 0)
            {
                try
                {
                    await _Regras.PesquisarPeriodo();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro na Pesquisa", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("- Selecione Ação", "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void cboTicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}