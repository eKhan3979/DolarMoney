using System;
using System.Windows.Controls;

namespace DolarMoney.Controles2
{
    public partial class UcrGraficosTendencias : UserControl
    {
        #region Público

        public event EventHandler Evento_Fechar;

        #endregion

        #region Construtor

        public UcrGraficosTendencias()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void ucmTitulo_Evento_Fechar(object sender, EventArgs e)
        {
            Evento_Fechar?.Invoke(this, e);
        }

        #endregion
    }
}