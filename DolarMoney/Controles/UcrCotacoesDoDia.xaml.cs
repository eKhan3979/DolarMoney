using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

using DolarMoney.Comum;

namespace DolarMoney.Controles
{
    public partial class UcrCotacoesDoDia : UserControl
    {
        #region Variáveis da Classe

        private CotacaoDoDiaRegras _Regras;
        private CotacaoDoDiaViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrCotacoesDoDia()
        {
            InitializeComponent();

            _ViewModel = new CotacaoDoDiaViewModel();
            _Regras = new CotacaoDoDiaRegras(ref _ViewModel);

            DataContext = _ViewModel;

            ThreadStart tstInicializar = new ThreadStart(Inicializar);
            Thread thrInicializar = new Thread(tstInicializar);
            thrInicializar.IsBackground = true;
            thrInicializar.Start();
        }

        private async void Inicializar()
        {
            try
            {
                await _Regras.Iniciar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos

        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            _ViewModel.CotacaoEdit.Cotacao = RotinasGerais.ParaDecimal(tbxCotacao.Text);

            if (_Regras.PodeGravar())
            {
                try
                {
                    if (await _Regras.Gravar())
                    {
                        _Regras.Limpar();
                    }
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro na Gravação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Stop);

                switch (_ViewModel.ErroIndex)
                {
                    case 1: cboTicker.Focus(); break;
                    case 2: dtpCotacoes.Focus(); break;
                    case 3: tbxCotacao.Focus(); break;
                }
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            _Regras.Limpar();
        }

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            //if (_ViewModel.DataPesquisa != _ViewModel.DataCotacao)
            //{
                try
                {
                    await _Regras.PesquisaComparativa();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Pesquisar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            //}
        }

        private void cboTicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Regras.CotacaoSelecionadaPorInvestimento();

            if (_ViewModel.CotacaoEdit.Cotacao != null)
                tbxCotacao.Text = _ViewModel.CotacaoEdit.Cotacao.Value.ToString("0.#0");
            else
                tbxCotacao.Text = "";
        }

        private void imgEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _Regras.CotacaoSelecionada((CotacaoPercentualDTO)((Image)sender).DataContext, 
                                       _ViewModel.DataCotacao.ToString("dd/MM/yyyy"));
            
            if (_ViewModel.CotacaoEdit.Cotacao != null)
                tbxCotacao.Text = _ViewModel.CotacaoEdit.Cotacao.Value.ToString("0.#0");
            else
                tbxCotacao.Text = "";

            tbxCotacao.Focus();
        }

        private void tbxCotacao_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RotinasGerais.SoValorEmMoeda(ref e);
        }

        private void tbxCotacao_LostFocus(object sender, RoutedEventArgs e)
        {
            RotinasGerais.TextBoxParaMoeda(ref tbxCotacao);
        }

        #endregion
    }
}