using System;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Win32;

using DolarMoney.Model;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles2
{
    public partial class UcrInvesting : UserControl
    {
        #region Variáveis da Classe

        private InvestingRegras _Regras;
        private InvestingViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrInvesting()
        {
            InitializeComponent();

            _ViewModel = new InvestingViewModel();
            _Regras = new InvestingRegras(ref _ViewModel);

            this.DataContext = _ViewModel;

            ThreadStart tstInicializar = new ThreadStart(Inicializacao);
            Thread thrInicializar = new Thread(tstInicializar);
            thrInicializar.IsBackground = true;
            thrInicializar.Start();
        }

        private async void Inicializacao()
        {
            try
            {
                if (!await _Regras.Inicializar())
                    throw new Exception(_ViewModel.ErroMsg.ToString());
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro na Inicialização", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos

        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await _Regras.Gravar())
                    MessageBox.Show("Lista de Cotações Gravada !", "Importação Investing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    if (_ViewModel.ErroIndex > 0)
                        throw new Exception(_ViewModel.ErroMsg.ToString());
                    else
                        throw new Exception("- Gravação sem sucesso !");
                }
                    
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro na Gravação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdHtml = new OpenFileDialog();

            ofdHtml.DefaultExt = ".html";
            ofdHtml.Title = "Selecione o HTML com cotações";
            ofdHtml.Filter = "HTML|*.html";

            if (ofdHtml.ShowDialog() == true)
            {
                _ViewModel.PathHTML = ofdHtml.FileName;

                try
                {
                    _Regras.Importacao();
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Importação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion
    }
}