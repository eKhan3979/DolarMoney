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
    public partial class UcrIFR : UserControl
    {
        #region Variáveis da Classe

        private IFRRegras _Regras;
        private IFRViewModel _ViewModel;

        #endregion

        #region Construtor

        public UcrIFR()
        {
            InitializeComponent();

            if (_ViewModel == null)
            {
                _ViewModel = new IFRViewModel();

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
                _Regras = new IFRRegras(ref _ViewModel);

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

        private void calcularIFR()
        {
            try
            {
                _Regras.CalcularIFR();

                plotarCotacoes();
                plotarIFR();
            }
            catch (Exception excErro)
            {
                MessageBox.Show(excErro.Message, "Erro Calcular IFR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal naoNull(decimal? dcmValor)
        {
            return ((dcmValor != null) ? dcmValor.Value : 0);
        }

        private void plotarCotacoes()
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

            sctGrafico.Refresh();
        }

        private void plotarIFR()
        {
            List<IPlottable> lstGraficos = sctIFR.Plot.PlottableList;

            if (lstGraficos.Count > 0)
            {
                for (int intGrafico = lstGraficos.Count - 1; intGrafico >= 0; intGrafico--)
                    sctIFR.Plot.Remove(lstGraficos[intGrafico]);
            }

            double?[] xs = new double?[_ViewModel.ListaIFR.Count];
            double?[] ys = new double?[_ViewModel.ListaIFR.Count];

            for (int intCotacao = 0; intCotacao < _ViewModel.ListaIFR.Count; intCotacao++)
            {
                xs[intCotacao] = intCotacao + 1;
                ys[intCotacao] = (double)_ViewModel.ListaIFR[intCotacao].IFR;
            }

            sctIFR.Plot.Grid.XAxis.Min = 1;
            sctIFR.Plot.Grid.XAxis.Max = xs.Length;

            sctIFR.Plot.Grid.YAxis.Min = 0;
            sctIFR.Plot.Grid.YAxis.Max = 100;

            sctIFR.Plot.Add.Scatter(xs, ys);

            xs = new double?[2] { 0, _ViewModel.ListaIFR.Count };
            ys = new double?[2] { 50, 50 };

            sctIFR.Plot.Add.Scatter(xs, ys);

            xs = new double?[2] { 0, _ViewModel.ListaIFR.Count };
            ys = new double?[2] { 30, 30 };

            sctIFR.Plot.Add.Scatter(xs, ys);

            xs = new double?[2] { 0, _ViewModel.ListaIFR.Count };
            ys = new double?[2] { 70, 70 };

            sctIFR.Plot.Add.Scatter(xs, ys);

            sctIFR.Refresh();
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
                    /*
                    _Regras.CalcularMediasAritmeticas();
                    _Regras.CalcularMediasAritmeticas(false);
                    _Regras.CalcularMACD();

                    plotar();
                    plotarHistograma();
                    */

                    calcularIFR();
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