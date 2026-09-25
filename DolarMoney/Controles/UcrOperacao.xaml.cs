using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using DolarMoney.Comum;
using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles
{
    public partial class UcrOperacao : UserControl
    {
        #region Variáveis da Classe

        private OperacaoRegras _Regras;
        private OperacaoViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrOperacao()
        {
            InitializeComponent();
        }

        protected async override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if ((_ViewModel == null) || (!_ViewModel.Iniciado))
            {
                try
                {
                    _ViewModel = new OperacaoViewModel();

                    _Regras = new OperacaoRegras(ref _ViewModel);

                     DataContext = _ViewModel;

                     await _Regras.Iniciar();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Inicialização", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        #endregion

        #region Eventos

        private async void btnCadGravar_Click(object sender, RoutedEventArgs e)
        {
            if (_Regras.PodeGravar())
            {
                if (MessageBox.Show("- Confirma a gravação ?", "Cadastro de Operações", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (await _Regras.Gravar())
                        {
                            btnCadLimpar_Click(null, new RoutedEventArgs());
                        }
                    }
                    catch (Exception excErro)
                    {
                        MessageBox.Show(excErro.Message, "Erro na gravação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void btnCadLimpar_Click(object sender, RoutedEventArgs e)
        {
            _Regras.Limpar();

            tbxCadQuantidade.Text = "";
            tbxCadValorTotal.Text = "";
        }

        private void btnCadExcluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _Regras.Pesquisar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Pesquisar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cboTipoInvestimento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Regras.CarregarInvestimentos();
        }

        private async void imgDetalhes_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (grdOperacaoInvestimento.Visibility != Visibility.Visible)
                    grdOperacaoInvestimento.Visibility = Visibility.Visible;

                _ViewModel.InvestimentoSelecionado = (EstoqueDTO)((Image)sender).DataContext;

                ucrOperacaoInvestimento.PublicoTitulo($"Operações do {_ViewModel.InvestimentoSelecionado.Ticker}");

                if (!await _Regras.CarregarOperacoesInvestimento())
                    throw new Exception("- Não foi possível carregar Operações !");
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Pesquisa", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void imgEdit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void tbxCadQuantidade_LostFocus(object sender, RoutedEventArgs e)
        {
            //RotinasGerais.TextBoxParaMoeda(ref tbxCadQuantidade);

            _ViewModel.Cadastro.Quantidade = RotinasGerais.ParaDecimal(tbxCadQuantidade.Text);
        }

        private void tbxCadQuantidade_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            RotinasGerais.SoValorEmMoeda(ref e);
        }

        private void tbxCadTaxa_LostFocus(object sender, RoutedEventArgs e)
        {
            RotinasGerais.TextBoxParaMoeda(ref tbxCadTaxa);

            _ViewModel.Cadastro.Taxa = RotinasGerais.ParaDecimal(tbxCadTaxa.Text);
        }

        private void tbxCadTaxa_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            RotinasGerais.SoValorEmMoeda(ref e);
        }

        private void tbxCadValorTotal_LostFocus(object sender, RoutedEventArgs e)
        {
            RotinasGerais.TextBoxParaMoeda(ref tbxCadValorTotal);

            _ViewModel.Cadastro.ValorTotal = RotinasGerais.ParaDecimal(tbxCadValorTotal.Text);
        }

        private void tbxCadValorTotal_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            RotinasGerais.SoValorEmMoeda(ref e);
        }

        private void ucrOperacaoInvestimento_Evento_Fechar(object sender, EventArgs e)
        {
            grdOperacaoInvestimento.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}