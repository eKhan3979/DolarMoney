using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ScottPlot;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.Regras;
using DolarMoney.ViewModel;

namespace DolarMoney.Controles2
{
    public partial class UcrMACD : UserControl
    {
        #region Variáveis da Classe

        private MACDRegras _Regras;
        private MACDViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrMACD()
        {
            InitializeComponent();

            if (_ViewModel == null)
            {
                _ViewModel = new MACDViewModel();

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
                _Regras = new MACDRegras(ref _ViewModel);

                if (!await _Regras.Iniciar())
                    throw new Exception("- Não Inicializado !");
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Inicializar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private

        private decimal naoNull(decimal? dcmValor)
        {
            return ((dcmValor != null) ? dcmValor.Value : 0);
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

            //sctGrafico.Plot.XLabel("Dia");
            //sctGrafico.Plot.YLabel("Cotações");
            //sctGrafico.Plot.Title(_ViewModel.AcaoSelecionada.Descricao);

            sctGrafico.Plot.Grid.XAxis.Min = 1;
            sctGrafico.Plot.Grid.XAxis.Max = xs.Length;

            double? dblMax = (double)_ViewModel.ListaCotacoes.Where(t => t.ValorCotacao != null).Max(t => t.ValorCotacao);
            double? dblMin = (double)_ViewModel.ListaCotacoes.Where(t => t.ValorCotacao != null).Min(t => t.ValorCotacao);

            double dblFolga = 1;

            if ((dblMax != null) && (dblMin != null))
                dblFolga = (dblMax.Value - dblMin.Value) / 20;

            if (dblMax != null)
                sctGrafico.Plot.Grid.YAxis.Max = dblMax.Value + dblFolga;

            if (dblMin != null)
                sctGrafico.Plot.Grid.YAxis.Min = dblMin.Value - dblFolga;

            sctGrafico.Plot.Add.Scatter(xs, ys);

            xs = new double?[_ViewModel.ListaEMA1.Count];
            ys = new double?[_ViewModel.ListaEMA1.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaEMA1.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = (double)naoNull(_ViewModel.ListaEMA1[intCotacao].ValorCotacao);
            }

            sctGrafico.Plot.Add.Scatter(xs, ys);

            xs = new double?[_ViewModel.ListaEMA2.Count];
            ys = new double?[_ViewModel.ListaEMA2.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaEMA2.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = (double)naoNull(_ViewModel.ListaEMA2[intCotacao].ValorCotacao);
            }

            sctGrafico.Plot.Add.Scatter(xs, ys);

            sctGrafico.Refresh();
        }

        private void plotarHistograma()
        {
            List<IPlottable> lstGraficos = sctHistograma.Plot.PlottableList;

            if (lstGraficos.Count > 0)
            {
                for (int intGrafico = lstGraficos.Count - 1; intGrafico >= 0; intGrafico--)
                    sctHistograma.Plot.Remove(lstGraficos[intGrafico]);
            }

            //Linha de Sinal
            double?[] xs = new double?[_ViewModel.ListaMACD.Count];
            double?[] ys = new double?[_ViewModel.ListaMACD.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaMACD.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = _ViewModel.ListaMACD[intCotacao].ValorMACD;
            }

            sctHistograma.Plot.Grid.XAxis.Min = 1;
            sctHistograma.Plot.Grid.XAxis.Max = xs.Length;

            double? dblMax = (double)_ViewModel.ListaMACD.Max(t => t.ValorMACD);
            double? dblMin = (double)_ViewModel.ListaMACD.Min(t => t.ValorMACD);

            double dblFolga = 1.0;

            if ((dblMax != null) && (dblMin != null))
                dblFolga = (dblMax.Value - dblMin.Value) / 20.0;

            if (dblMax != null)
                sctHistograma.Plot.Grid.YAxis.Max = dblMax.Value + dblFolga;

            if (dblMin != null)
                sctHistograma.Plot.Grid.YAxis.Min = dblMin.Value - dblFolga;

            sctHistograma.Plot.Add.Scatter(xs, ys);

            //Linha de Sinal
            xs = new double?[_ViewModel.ListaMACD.Count];
            ys = new double?[_ViewModel.ListaMACD.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaMACD.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = _ViewModel.ListaMACD[intCotacao].ValorEMAMACD;
            }

            sctHistograma.Plot.Grid.XAxis.Min = 1;
            sctHistograma.Plot.Grid.XAxis.Max = xs.Length;

            dblMax = (double)_ViewModel.ListaMACD.Max(t => t.ValorEMAMACD);
            dblMin = (double)_ViewModel.ListaMACD.Min(t => t.ValorEMAMACD);

            if ((dblMax != null) && (dblMin != null))
                dblFolga = (dblMax.Value - dblMin.Value) / 20.0;

            if (dblMax != null)
                sctHistograma.Plot.Grid.YAxis.Max = dblMax.Value + dblFolga;

            if (dblMin != null)
                sctHistograma.Plot.Grid.YAxis.Min = dblMin.Value - dblFolga;

            sctHistograma.Plot.Add.Scatter(xs, ys);

            Color corLinha = new Color(32, 32, 180, 64);
            Color corBar = new Color(64, 64, 220, 32);

            for (int intDia = 0; intDia < _ViewModel.ListaMACD.Count; intDia++)
            {
                Bar histograma = new Bar();

                histograma.Position = intDia;
                histograma.Value = _ViewModel.ListaMACD[intDia].ValorMACD -
                                   _ViewModel.ListaMACD[intDia].ValorEMAMACD;
                histograma.LineColor = corLinha;
                histograma.FillColor = corBar;

                sctHistograma.Plot.Add.Bar(histograma);
            }

            sctHistograma.Refresh();
        }

        #endregion

        #region Eventos

        private void ucrAnalise_Evento_Gravar(object sender, EventArgs e)
        {

        }

        private void ucrParametros_Evento_Analises(object sender, EventArgs e)
        {

        }

        private async void ucrParametros_Evento_Pesquisar(object sender, EventArgs e)
        {
            try
            {
                if (await _Regras.CarregarCotacoes())
                {
                    _Regras.CalcularMediasAritmeticas();
                    _Regras.CalcularMediasAritmeticas(false);
                    _Regras.CalcularMACD();

                    plotar();
                    plotarHistograma();
                }
                else
                {
                    throw new Exception("- Cotações NÃO carregadas !");
                }
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Pesquisar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}