using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DolarMoney.Comum
{
    public partial class UcrTitulo : UserControl
    {
        #region Público

        public event EventHandler Evento_Fechar;

        public HorizontalAlignment Publico_TitleAlinhamentoH
        {
            get { return tbkTitulo.HorizontalAlignment; }
            set { tbkTitulo.HorizontalAlignment = value; }
        }

        public string Publico_Titulo
        {
            get { return tbkTitulo.Text; }
            set { tbkTitulo.Text = value; }
        }

        public Visibility Publico_VisibilityClose
        {
            get { return imgFechar.Visibility; }
            set { imgFechar.Visibility = value; }
        }

        #endregion

        #region Construtor

        public UcrTitulo()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void imgFechar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Evento_Fechar != null)
                Evento_Fechar(null, new EventArgs());
        }

        #endregion
    }
}