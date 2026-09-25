using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using DolarMoney.Model;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles2
{
    public partial class UcrIR : UserControl
    {
        #region Variáveis da Classe

        private IRViewModel _ViewModel;
        private IRRegras _Regras;

        #endregion

        #region Construtor

        public UcrIR()
        {
            InitializeComponent();

            _ViewModel = new IRViewModel();

            DataContext = _ViewModel;

            try
            {
                _Regras = new IRRegras(ref _ViewModel);

                _Regras.Iniciar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _Regras.Pesquisar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Pesquisa", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}