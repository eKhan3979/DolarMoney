using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace DolarMoney.Comum
{
    public static class RotinasGerais
    {
        public static string formatarMoedaParaGravar(string strValor)
        {
            return (!string.IsNullOrWhiteSpace(strValor) ? strValor.Replace(",", ".") : "");
        }

        public static decimal ParaDecimal(object valor)
        {
            decimal dcmRetorno = 0;

            if (valor != null)
                decimal.TryParse(valor.ToString(), out dcmRetorno);

            return dcmRetorno;
        }

        public static string ParaDecimalString(object valor)
        {
            decimal dcmRetorno = 0;

            if (valor != null)
                decimal.TryParse(valor.ToString(), out dcmRetorno);

            //return dcmRetorno.ToString("N", _emBrazil);
            return dcmRetorno.ToString("N");
        }

        public static int ParaInteiro(object valor)
        {
            int intRetorno = 0;

            if (valor != null)
                int.TryParse(valor.ToString(), out intRetorno);

            return intRetorno;
        }

        public static string ParaInteiroString(object valor)
        {
            int intRetorno = 0;

            if (valor != null)
                int.TryParse(valor.ToString(), out intRetorno);

            return intRetorno.ToString();
        }

        public static string ParaMoedaString(object valor)
        {
            decimal dcmRetorno = 0;

            if (valor != null)
                decimal.TryParse(valor.ToString(), out dcmRetorno);

            //return dcmRetorno.ToString("C", _emBrazil);
            return dcmRetorno.ToString("C").Replace("R$", "").Trim();
        }

        public static void SoValorEmMoeda(ref TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void TextBoxInteiro(ref TextBox tbxInteiro)
        {
            if (int.TryParse(tbxInteiro.Text, out int valor))
            {
                tbxInteiro.Text = valor.ToString();
            }
        }

        public static void TextBoxParaMoeda(ref TextBox tbxMoeda)
        {
            if (decimal.TryParse(tbxMoeda.Text, out decimal valor))
            {
                tbxMoeda.Text = valor.ToString("F");
            }
        }
    }
}