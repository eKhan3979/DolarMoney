using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DolarMoney.Comum
{
    public partial class UcpBarraWindow : UserControl
    {
        #region Público

        public event EventHandler Evento_Fechar,
                                  Evento_Maximizar,
                                  Evento_Minimizar;

        public string Publico_Titulo
        {
            get { return tbkTitulo.Text; }
            set { tbkTitulo.Text = value; }
        }

        public bool Publico_WindowMaximized
        {
            get { return mWindowMaximized; }
            set { mWindowMaximized = value; }
        }

        #endregion

        #region Variáveis

        private bool mWindowMaximized = false;

        #endregion

        #region Construtor

        public UcpBarraWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            if (Evento_Fechar != null)
                Evento_Fechar(null, new EventArgs());
        }

        private void BtnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            mWindowMaximized = !mWindowMaximized;

            if (mWindowMaximized)
            {
                btnNormal.Visibility = Visibility.Visible;
                btnMaximizar.Visibility = Visibility.Hidden;
            }
            else
            {
                btnNormal.Visibility = Visibility.Hidden;
                btnMaximizar.Visibility = Visibility.Visible;
            }

            if (Evento_Maximizar != null)
                Evento_Maximizar(null, new EventArgs());
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            if (Evento_Minimizar != null)
                Evento_Minimizar(null, new EventArgs());
        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {
            BtnMaximizar_Click(null, new RoutedEventArgs());
        }

        #endregion
    }
}