using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles2
{
    public partial class UcrImportVolumes : UserControl
    {
        #region Variáveis da Classe

        private ImportVolumesViewModel _ViewModel;
        private ImportVolumesRegras _Regras;

        #endregion

        #region Construtor

        public UcrImportVolumes()
        {
            InitializeComponent();

            _ViewModel = new ImportVolumesViewModel();

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
                _ViewModel.ErroIndex = 0;

                _Regras = new ImportVolumesRegras(ref _ViewModel);
                _Regras.Evento_RegistrosGravados += Regras_Evento_RegistrosGravados;

                if (await _Regras.Iniciar())
                {

                }
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
            if (_Regras.PodeImportar())
            {
                if (MessageBox.Show("- Confirma Importação ?", "Dolar MONEY !!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Dispatcher.Invoke(new Action(() => { grdGravacoes.Visibility = Visibility.Visible; }));

                        if (await _Regras.Gravar())
                        {
                            MessageBox.Show("Importação Finalizada !", _ViewModel.ListaVolumes.Count.ToString() + " gravados !", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                            _Regras.Limpar();
                        }
                    }
                    catch (Exception excErro)
                    {
                        MessageBox.Show(excErro.Message, "Erro na Importação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        Dispatcher.Invoke(new Action(() => { grdGravacoes.Visibility = Visibility.Hidden; }));
                    }
                }
            }
            else
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdCSV = new OpenFileDialog();

            ofdCSV.DefaultExt = "xlsx";
            ofdCSV.Filter = "CSV|*.csv";
            ofdCSV.Title = "Selecione a planilha CVS a Importar ";

            if (ofdCSV.ShowDialog() == true)
            {
                try
                {
                    _ViewModel.PathCSV = ofdCSV.FileName;

                    _Regras.LerCSV();

                    tbxRegistros.Text = _ViewModel.ListaVolumes.Count.ToString("#,##0");
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Ler XLSX", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Regras_Evento_RegistrosGravados(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (sender != null)
                    tbxGravados.Text = ((int)sender).ToString("#,##0");
                else
                    tbxGravados.Text = "";
            }));
        }

        #endregion
    }
}