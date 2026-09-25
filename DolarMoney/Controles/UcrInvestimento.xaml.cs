using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles
{
    public partial class UcrInvestimento : UserControl
    {
        #region Variáveis da Classe

        private InvestimentoRegras _Regras;
        private InvestimentoViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrInvestimento()
        {
            InitializeComponent();
        }

        protected async override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (_ViewModel == null)
            {
                try
                {
                    _ViewModel = new InvestimentoViewModel();

                    _Regras = new InvestimentoRegras(ref _ViewModel);

                    DataContext = _ViewModel;

                    _ViewModel.Iniciado = await _Regras.Iniciar();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro na Inicialização", MessageBoxButton.YesNo, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region Eventos

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _Regras.Edit((InvestimentoDTO)((Button)sender).DataContext);
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            if (_Regras.PodeGravar())
            {
                if (MessageBox.Show("Confirma a gracação ?", "Dolar Money", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (_Regras.Gravar())
                            _Regras.Limpar();
                    }
                    catch (Exception excErro)
                    {
                        MessageBox.Show(excErro.Message, "Erro na Gravação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            _Regras.Limpar();
        }

        #endregion
    }
}