using System;
using System.Windows;
using System.Windows.Controls;

namespace DolarMoney.Controles2
{
    public partial class UcrGraficosAnalise : UserControl
    {
        #region Público

        public event EventHandler Evento_Gravar;

        #endregion

        #region Construtor

        public UcrGraficosAnalise()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Evento_Gravar?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}