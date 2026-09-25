using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DolarMoney.Controles
{
    public partial class UcrOperacaoInvestimento : UserControl
    {
        #region Público

        public event EventHandler Evento_Fechar;

        public void PublicoTitulo(string strTitulo)
        {
            utlOperacaoInvestimento.Publico_Titulo = strTitulo;
        }

        #endregion

        #region Construtor

        public UcrOperacaoInvestimento()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void utlOperacaoInvestimento_Evento_Fechar(object sender, EventArgs e)
        {
            Evento_Fechar?.Invoke(null, new EventArgs());
        }

        #endregion
    }
}