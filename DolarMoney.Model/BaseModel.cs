using System;
using System.ComponentModel;

namespace DolarMoney.Model
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string strPropriedade)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropriedade));
        }

        public string Dd_Mm_Yyyy(string strYyyy_Mm_Dd)
        {
            try { return strYyyy_Mm_Dd.Substring(8, 2) + "/" +
                         strYyyy_Mm_Dd.Substring(5, 2) + "/" +
                         strYyyy_Mm_Dd.Substring(0, 4); }
            catch { return ""; }
        }

        public string Dd_Mm_Yyyy(DateTime dtmConverter)
        {
            try
            {
                return dtmConverter.ToString("dd/MM/yyyy");
            }
            catch { return ""; }
        }

        public string ParaDecimal(decimal? dcmValor, int intDecimais = 2)
        {
            if ((dcmValor != null) && (dcmValor.Value > 0))
            {
                string strMask = "0.";

                for (int intDec = 1; intDec < intDecimais; intDec++)
                    strMask += "#";

                strMask += "0";

                return dcmValor.Value.ToString(strMask);
            }
            else
                return "";
        }

        public string ParaMoeda(decimal dcmValor, bool cifrao = false, bool setedecimais = false)
        {
            if (dcmValor != 0)
                if (!setedecimais)
                    return ((cifrao) ? "R$ " : "") + dcmValor.ToString("#,##0.#0");
                else
                    return ((cifrao) ? "R$ " : "") + dcmValor.ToString("#,##0.#0#####");
            else
                return "-";
        }

        public string ParaMoeda(decimal? dcmValor, bool cifrao = false)
        {
            if ((dcmValor != null) && (dcmValor != 0))
                return ((cifrao) ? "R$ " : "") + dcmValor.Value.ToString("#,##0.#0");
            else
                return "-";
        }

        public string ParaPercentagem(decimal dcmValor)
        {
            if (dcmValor != 0)
                return dcmValor.ToString("#,##0.#0") + "%";
            else
                return "-";
        }
    }
}