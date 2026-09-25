using Microsoft.Win32;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles
{
    public partial class UcrCotacoesImport : UserControl
    {
        #region Variáveis da Classe

        private CotacaoImportRegras _Regras;
        private CotacaoImportViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrCotacoesImport()
        {
            InitializeComponent();
        }

        protected async override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if ((_ViewModel == null) ||
                (!_ViewModel.Iniciado))
            {
                try
                {
                    _ViewModel = new CotacaoImportViewModel();

                     DataContext = _ViewModel;

                    _Regras = new CotacaoImportRegras(ref _ViewModel);
                    _Regras.Evento_ImportacaoFinalizada += Regras_Evento_ImportacaoFinalizada;

                    _ViewModel.Iniciado = await _Regras.Iniciar();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region Private

        private async void gravarCotacoes()
        {
            try
            {
                await _Regras.Gravar();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Erro na gravação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos

        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            if (_Regras.PodeGravar())
            {
                if (MessageBox.Show("- Confirma a gravação ?", "Lista de Cotações", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Dispatcher.Invoke(() =>
                    {
                        grdGravacoes.Visibility = Visibility.Visible;
                    });

                    ThreadStart tstGravar = new ThreadStart(gravarCotacoes);
                    Thread thrGravar = new Thread(tstGravar);
                    thrGravar.IsBackground = true;
                    thrGravar.Start();

                    /*
                    try
                    {
                        grdGravacoes.Visibility = Visibility.Visible;

                        await _Regras.Gravar();

                        MessageBox.Show("- Lista de Cotações gravada", "Dolar MONEY !!!", MessageBoxButton.OK, MessageBoxImage.Error);


                    }
                    catch (Exception excErro)
                    {
                        MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Erro na gravação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    */
                }
            }
            else
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImport = new OpenFileDialog();

            ofdImport.DefaultExt = "*.json|*.json";
            ofdImport.Title = "Selecione o arquivo JSON a importar";
            
            if (ofdImport.ShowDialog() == true)
            {
                _ViewModel.PathImport = ofdImport.FileName;

                try
                {
                    _Regras.LerJSON();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro na Importação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Regras_Evento_ImportacaoFinalizada(object? sender, EventArgs e)
        {
            if (sender?.ToString() == "OK")
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("- Lista de Cotações gravada", "Dolar MONEY !!!", MessageBoxButton.OK, MessageBoxImage.Information);

                    grdGravacoes.Visibility = Visibility.Hidden;
                });
            else
                MessageBox.Show("?", "?");
        }

        #endregion
    }
}