
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ScottPlot;
using ScottPlot.WPF;


using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles2
{
    public partial class UcrGraficos : UserControl
    {
        #region Variáveis da Classe

        private GraficosViewModel _ViewModel;
        private GraficosRegras _Regras;

        #endregion

        #region Construtor

        public UcrGraficos()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if ((_ViewModel == null) || (!_ViewModel.Iniciado))
            {
                if (_ViewModel == null)
                {
                    _ViewModel = new GraficosViewModel();
                    DataContext = _ViewModel;
                }

                ThreadStart tstInicializar = new ThreadStart(Inicializar);
                Thread thrInicializar = new Thread(tstInicializar);
                thrInicializar.IsBackground = true;
                thrInicializar.Start();
            }
        }

        #endregion

        #region Private

        private async void Inicializar()
        {
            if ((_ViewModel != null) && (!_ViewModel.Iniciado))
            {
                try
                {
                    if (_Regras == null)
                        _Regras = new GraficosRegras(ref _ViewModel);

                    await _Regras.Iniciar();

                    _ViewModel.Iniciado = true;
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Inicializar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void plotar()
        {
            List<IPlottable> lstGraficos = sctGrafico.Plot.PlottableList;

            if (lstGraficos.Count > 0)
            {
                for (int intGrafico = lstGraficos.Count - 1; intGrafico >= 0; intGrafico--)
                    sctGrafico.Plot.Remove(lstGraficos[intGrafico]);
            }

            double?[] xs = new double?[_ViewModel.ListaCotacoes.Count];
            double?[] ys = new double?[_ViewModel.ListaCotacoes.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaCotacoes.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = (double)_ViewModel.ListaCotacoes[intCotacao].ValorCotacao;
            }

            sctGrafico.Plot.XLabel("Dia");
            sctGrafico.Plot.YLabel("Cotações");
            sctGrafico.Plot.Title(_ViewModel.AcaoSelecionada.Descricao);

            sctGrafico.Plot.Grid.XAxis.Min = 1;
            sctGrafico.Plot.Grid.XAxis.Max = xs.Length;

            double? dblMax = (double)_ViewModel.ListaCotacoes.Where(t => t.ValorCotacao != null).Max(t => t.ValorCotacao);
            double? dblMin = (double)_ViewModel.ListaCotacoes.Where(t => t.ValorCotacao != null).Min(t => t.ValorCotacao);

            if (dblMax != null)
                sctGrafico.Plot.Grid.YAxis.Max = dblMax.Value;

            if (dblMin != null)
                sctGrafico.Plot.Grid.YAxis.Min = dblMin.Value;

            sctGrafico.Plot.Add.Scatter(xs, ys);

            xs = new double?[_ViewModel.ListaMM1.Count];
            ys = new double?[_ViewModel.ListaMM1.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaMM1.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = ((_ViewModel.ListaMM1[intCotacao].ValorCotacao != null) ? (double)_ViewModel.ListaMM1[intCotacao].ValorCotacao.Value
                                                                                         : null);
            }

            sctGrafico.Plot.Add.Scatter(xs, ys);

            xs = new double?[_ViewModel.ListaMM2.Count];
            ys = new double?[_ViewModel.ListaMM2.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaMM2.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = ((_ViewModel.ListaMM2[intCotacao].ValorCotacao != null) ? (double)_ViewModel.ListaMM2[intCotacao].ValorCotacao.Value
                                                                                         : null);
            }

            sctGrafico.Plot.Add.Scatter(xs, ys);

            sctGrafico.Refresh();
        }

        #endregion

        #region Eventos

        private async void ucrAnalise_Evento_Gravar(object sender, EventArgs e)
        {
            if (_Regras.PodeGravarAnalise())
            {
                if (MessageBox.Show("- Confirma a Gravação da Análise ?",
                                    "Dolar MONEY !!!",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Question,
                                     MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (await _Regras.GravarAnalise())
                            MessageBox.Show("- Análise gravada !", "Dolar MONEY !!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    catch (Exception excErro)
                    {
                        MessageBox.Show(excErro.Message, "Erro na Gravação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Error);

                ucrParametros.Focus();
            }
        }

        private async void ucrParametros_Evento_Analises(object sender, EventArgs e)
        {
            grdTendenciasAtuais.Visibility = Visibility.Visible;

            try
            {
                await _Regras.CarregarTendenciasAtuais();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Lista Tendências", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ucrParametros_Evento_Pesquisar(object sender, EventArgs e)
        {
            if (_Regras.PodePesquisar())
            {
                try
                {
                    if (await _Regras.CarregarCotacoes())
                    {
                        _Regras.CalcularMediaMovel();
                        _Regras.CalcularMediaMovel(false);

                        plotar();
                    }
                }
                catch (Exception excErro)
                {
                    MessageBox.Show(excErro.Message, "Erro Pesquisar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show(_ViewModel.ErroMsg.ToString(), "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ucrTendencias_Evento_Fechar(object sender, EventArgs e)
        {
            grdTendenciasAtuais.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}