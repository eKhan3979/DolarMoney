using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DolarMoney.Regras
{
    public class BaseRegras
    {
        public BaseRegras() { }

        public enum enumTipoInvestimento
        {
            Nenhum = 0,
            Acoes = 1,
            RendaFixaPrivada = 2
        };

        public string Dd_Mm_Yyyy(string strYyyy_Mm_Dd)
        {
            try
            {
                return strYyyy_Mm_Dd.Substring(8, 2) + "/" +
                       strYyyy_Mm_Dd.Substring(5, 2) + "/" +
                       strYyyy_Mm_Dd.Substring(0, 4);
            }
            catch { return ""; }
        }

        public string Dd_Mm_Yyyy(DateTime dtmConverter)
        {
            try
            {
                return (100 + dtmConverter.Day).ToString().Substring(1, 2) + "/" +
                       (100 + dtmConverter.Month).ToString().Substring(1, 2) + "/" +
                       (10000 + dtmConverter.Year).ToString().Substring(1, 4);
            }
            catch { return ""; }
        }

        public string GetEnumDescription(Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[]? attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }

        public DateTime? ParaDateTime(string strDd_Mm_Yyyy)
        {
            try { return DateTime.Parse(strDd_Mm_Yyyy); }
            catch { return null; }
        }

        public decimal ParaDecimal(string strValor)
        {
            try { return decimal.Parse(strValor); }
            catch { return 0; }
        }

        public decimal ParaDecimalCotistas(object objValor)
        {
            try
            {
                if (objValor != null)
                    return decimal.Parse(objValor.ToString().Replace(".", ","));
                else
                    return 0;
            }
            catch { return 0; }
        }

        public decimal ParaDecimalPonto(string strValor)
        {
            try
            {
                if ((1.1).ToString().Substring(1, 1) == ",")
                    return decimal.Parse(strValor.Replace("%", "").Trim());
                else
                    return decimal.Parse(strValor.Replace("%", "").Replace(".", "").Replace(",", ".").Trim());
            }
            catch { return 0; }
        }

        public int ParaInteiro(string strValor)
        {
            try { return Int32.Parse(strValor); }
            catch { return 0; }
        }

        public string ParaMoeda(decimal dcmValor,
                                   bool cifrao = false)
        {
            if (dcmValor != 0)
            {
                if (dcmValor > 0)
                    return ((cifrao) ? "R$ " : "") + dcmValor.ToString("#,##0.#0 ");
                else
                    return ((cifrao) ? "R$ " : "") + (-dcmValor).ToString("(#,##0.#0)");
            }
            else
                return "-";
        }

        public string ParaMoeda(decimal? dcmValor,
                                   bool cifrao = false)
        {
            if ((dcmValor != null) && (dcmValor != 0))
            {
                if (dcmValor > 0)
                    return ((cifrao) ? "R$ " : "") + dcmValor.Value.ToString("#,##0.#0 ");
                else
                    return ((cifrao) ? "R$ " : "") + (-dcmValor.Value).ToString("(#,##0.#0)");
            }
            else
                return "-";
        }

        public string ParaPercentual(decimal dcmValor,
                                        bool boolSimbolo = false)
        {
            try
            {
                if (dcmValor > 0)
                    return dcmValor.ToString("0.#0") + ((boolSimbolo) ? "%" : "");
                else if (dcmValor < 0)
                    return "(" + (-dcmValor).ToString("0.#0") + ((boolSimbolo) ? "%" : "") + ")";
                else
                    return "-";
            }
            catch { return "-"; }
        }

        public string Yyyy_Mm_Dd(string strDd_Mm_Yyyy)
        {
            try
            {
                return strDd_Mm_Yyyy.Substring(6, 4) + "/" +
                       strDd_Mm_Yyyy.Substring(3, 2) + "/" +
                       strDd_Mm_Yyyy.Substring(0, 2);
            }
            catch { return ""; }
        }

        public string Yyyy_Mm_Dd(DateTime dtpConverter)
        {
            try
            {
                return dtpConverter.ToString("yyyy/MM/dd");
            }
            catch { return ""; }
        }
    }
}