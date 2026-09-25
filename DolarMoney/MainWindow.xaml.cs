using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using DolarMoney.Controles;
using DolarMoney.Controles2;

namespace DolarMoney
{
    public partial class MainWindow : Window
    {
        #region Variáveis da Classe

        private UcrInvestimento _ucrInvestimento;
        private UcrOperacao _ucrOperacao;
        private UcrCotacoes _ucrCotacoes;
        private UcrCotacoesImport _ucrCotacoesImport;
        private UcrImportVolumes _ucrVolumesImport;
        private UcrCotacoesDoDia _ucrCotacoesDoDia;
        private UcrInvesting _ucrInvesting;

        private UcrGraficos _ucrGraficos;
        private UcrMACD _ucrMACD;
        private UcrIFR _ucrIFR;
        private UcrDonchian _ucrDonchian;
        private UcrIR _ucrIR;

        #endregion

        #region Construtor

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        #endregion

        #region Eventos

        private void mitCotacoes_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrCotacoes == null)
                _ucrCotacoes = new UcrCotacoes();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrCotacoes")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrCotacoes);

            _ucrCotacoes.Focus();
        }

        private void mitCotacoesDoDia_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrCotacoesDoDia == null)
                _ucrCotacoesDoDia = new UcrCotacoesDoDia();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrCotacoesDoDia")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrCotacoesDoDia);

            _ucrCotacoesDoDia.Focus();
        }

        private void mitCotacoesImport_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrCotacoesImport == null)
                _ucrCotacoesImport = new UcrCotacoesImport();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrCotacoesImport")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrCotacoesImport);

            _ucrCotacoesImport.Focus();
        }

        private void mitInvestimentos_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrInvestimento == null)
                _ucrInvestimento = new UcrInvestimento();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrInvestimento")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrInvestimento);

            _ucrInvestimento.Focus();
        }

        private void mitOperacoes_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrOperacao == null)
                _ucrOperacao = new UcrOperacao();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrOperacao")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrOperacao);

            _ucrOperacao.Focus();
        }

        private void mitMediaMovel_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrGraficos == null)
                _ucrGraficos = new UcrGraficos();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrGraficos")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrGraficos);

            _ucrGraficos.Focus();
        }

        private void mitMACD_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrMACD == null)
                _ucrMACD = new UcrMACD();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrMACD")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrMACD);

            _ucrMACD.Focus();
        }

        private void mitIFR_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrIFR == null)
                _ucrIFR = new UcrIFR();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrIFR")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrIFR);

            _ucrIFR.Focus();
        }

        private void mitFR_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrIR == null)
                _ucrIR = new UcrIR();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrIR")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrIR);

            _ucrIR.Focus();
        }

        private void mitCanaisDonchian_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrDonchian == null)
                _ucrDonchian = new UcrDonchian();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrDonchian")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrDonchian);

            _ucrDonchian.Focus();
        }

        private void mitInvesting_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrInvesting == null)
                _ucrInvesting = new UcrInvesting();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrInvesting")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrInvesting);

            _ucrInvesting.Focus();
        }

        private void mitVolume_Click(object sender, RoutedEventArgs e)
        {
            if (_ucrVolumesImport == null)
                _ucrVolumesImport = new UcrImportVolumes();

            bool boolOk = false;

            foreach (var controle in grdControles.Children)
            {
                if (((UserControl)controle).GetType().Name == "UcrVolumesImport")
                {
                    ((UserControl)controle).Visibility = Visibility.Visible;
                    boolOk = true;
                }
                else
                    ((UserControl)controle).Visibility = Visibility.Hidden;
            }

            if (!boolOk)
                grdControles.Children.Add(_ucrVolumesImport);

            _ucrVolumesImport.Focus();
        }

        private void utlMain_Evento_Fechar(object sender, EventArgs e)
        {
            Close();
        }

        private void utlMain_Evento_Maximizar(object sender, EventArgs e)
        {
            WindowState = ((WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal);
        }

        private void utlMain_Evento_Minimizar(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Minimized)
                WindowState = WindowState.Minimized;
        }

        private void utlMain_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion
    }
}