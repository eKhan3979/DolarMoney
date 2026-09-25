using MySqlConnector;
using System.Text;

namespace DolarMoney.DAO
{
    public class BaseDAO
    {
        #region Construtor

        public BaseDAO() { }

        #endregion

        #region Conexão

        public MySqlConnection GetConexao()
        {
            return new MySqlConnection($@"Server={Environment.GetEnvironmentVariable("MariaDBServer")};
Database={Environment.GetEnvironmentVariable("MariaDBDataBase")};
User Id={Environment.GetEnvironmentVariable("MariaDBUserID")};
Password={Environment.GetEnvironmentVariable("MariaDBPassword")};
Port={Environment.GetEnvironmentVariable("MariaDBPort")};");
        }

        #endregion

        #region Funções Date

        public string DateTimeParaDdMmYyyy(Nullable<DateTime> dtmConverter)
        {
            if (dtmConverter != null)
                return dtmConverter.Value.ToString("dd/MM/yyyy");
            else
                return null;
        }

        public string Dd_Mm_Yyyy(string strYyyy_Mm_Dd)
        {
            if (!string.IsNullOrWhiteSpace(strYyyy_Mm_Dd))
                return strYyyy_Mm_Dd.Substring(8, 2) + "/" +
                       strYyyy_Mm_Dd.Substring(5, 2) + "/" +
                       strYyyy_Mm_Dd.Substring(0, 4);
            else
                return "";
        }

        public DateTime ParaDateTime(object objValor)
        {
            DateTime dtmValor = DateTime.Now;

            try { DateTime.TryParse(objValor.ToString(), out dtmValor); }
            catch { }

            return dtmValor;
        }

        public string TiraVirgula(decimal dcmValor)
        {
            return dcmValor.ToString().Replace(",", ".");
        }

        public string TiraVirgula(decimal? dcmValor)
        {
            if (dcmValor != null)
                return dcmValor.Value.ToString().Replace(",", ".");
            else
                return "0";
        }

        public string Yyyy_Mm_Dd(DateTime? dtmConverter)
        {
            string strYyyy_Mm_Dd = "Null";

            try
            {
                if (dtmConverter != null)
                    strYyyy_Mm_Dd = "'" + dtmConverter.Value.ToString("yyyy/MM/dd") + "'";
            }
            catch { }

            return strYyyy_Mm_Dd;
        }

        public string Yyyy_Mm_Dd(string strDd_Mm_Yyyy)
        {
            string strYyyy_Mm_Dd = "Null";

            try
            {
                if (strDd_Mm_Yyyy != null)
                    strYyyy_Mm_Dd = "'" + strDd_Mm_Yyyy.Substring(6, 4) + "/" +
                                          strDd_Mm_Yyyy.Substring(3, 2) + "/" +
                                          strDd_Mm_Yyyy.Substring(0, 2) + "'";
            }
            catch { }

            return strYyyy_Mm_Dd;
        }

        public string Yyyy_Mm_Dd_Vazio(string strDd_Mm_Yyyy)
        {
            string strYyyy_Mm_Dd = "";

            try
            {
                if (strDd_Mm_Yyyy != null)
                    strYyyy_Mm_Dd = strDd_Mm_Yyyy.Substring(6, 4) + "/" +
                                    strDd_Mm_Yyyy.Substring(3, 2) + "/" +
                                    strDd_Mm_Yyyy.Substring(0, 2);
            }
            catch { }

            return "'" + strYyyy_Mm_Dd + "'";
        }

        public string Yyyy_Mm_Dd(DateTime dtmConverter)
        {
            return "'" + dtmConverter.ToString("yyyy/MM/dd") + "'";
        }

        #endregion

        #region Funções Conversão

        public decimal ParaDecimal(string strValor)
        {
            try { return decimal.Parse(strValor.Replace(".", "").Replace(",", ".")); }
            catch { return 0; }
        }

        public decimal? ParaDecimal(object objValor)
        {
            try
            {
                return decimal.Parse(objValor.ToString());
            }
            catch { return null; }
        }

        public decimal ParaDecimalZero(object objValor)
        {
            try
            {
                return decimal.Parse(objValor.ToString());
            }
            catch { return 0; }
        }

        public decimal? ParaDecimalNull(object objValor)
        {
            if (objValor != System.DBNull.Value)
            {
                try
                {
                    return decimal.Parse(objValor.ToString());
                }
                catch { return null; }
            }
            else
                return null;
        }

        public string ParaDecimalSQLServer(string strValor)
        {
            try { return strValor.Replace(".", "").Replace(",", "."); }
            catch { return "0.00"; }
        }

        public int ParaInteiro(object objValor)
        {
            try
            {
                if (objValor != System.DBNull.Value)
                    return int.Parse(objValor.ToString());
                else
                    return 0;
            }
            catch { return 0; }
        }

        public int? ParaInteiroNull(object objValor)
        {
            try
            {
                if (objValor != System.DBNull.Value)
                    return int.Parse(objValor.ToString());
                else
                    return null;
            }
            catch { return null; }
        }

        public short ParaShort(object objValor)
        {
            short shtValor = 0;

            try { shtValor = Convert.ToInt16(objValor); }
            catch { }

            return shtValor;
        }

        public string ParaStringSpaceLeft(string strValor, int intLength = 5)
        {
            string strReturn = "          " + strValor;

            return strReturn.Substring(strReturn.Length - intLength, intLength);
        }

        public string ParaStringSpaceRight(string strValor, int intLength = 5)
        {
            string strReturn = strValor + "                    ";

            return strReturn.Substring(0, intLength);
        }

        public string ParaString(object objValor)
        {
            if (objValor != null)
                return objValor.ToString().Trim();
            else
                return string.Empty;
        }

        public string ParaString(decimal? dcmValor)
        {
            return (((dcmValor != null) && (dcmValor.Value != 0)) ? dcmValor.Value.ToString("#.#0") : "");
        }

        #endregion
    }
}