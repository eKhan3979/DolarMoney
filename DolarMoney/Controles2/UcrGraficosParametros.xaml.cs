using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DolarMoney.Controles2
{
    public partial class UcrGraficosParametros : UserControl
    {
        #region Público

        public event EventHandler Evento_Analises,
                                  Evento_Pesquisar;

        public string Publico_Titulo
        {
            get => utlGraficos.Publico_Titulo;
            set
            {
                utlGraficos.Publico_Titulo = value;
            }
        }

        #endregion

        #region Construtor

        public UcrGraficosParametros()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void btnAnalise_Click(object sender, RoutedEventArgs e)
        {
            Evento_Analises?.Invoke(this, EventArgs.Empty);
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Evento_Pesquisar?.Invoke(null, new EventArgs());
        }

        private void cboAcao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboDiasMM1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboDiasMM2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void utlGraficos_Evento_Fechar(object sender, EventArgs e)
        {

        }

        #endregion
    }
}