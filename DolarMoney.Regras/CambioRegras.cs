using System;
using System.Text;

using DolarMoney.Model;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class CambioRegras: BaseRegras
    {
        #region Variáveis da Classe

        private CambioViewModel _vm;

        #endregion

        #region Construtor

        public CambioRegras(ref CambioViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private string yyyy_Mm_Dd(string strDdMmYyyy)
        {
            if (strDdMmYyyy.Length == 8)
                return strDdMmYyyy.Substring(4, 4) + "/" +
                       strDdMmYyyy.Substring(2, 2) + "/" +
                       strDdMmYyyy.Substring(0, 2);
            else
                return "";
        }

        #endregion 

        #region Público

        public event EventHandler Evento_Gravacoes;

        public void Gravar()
        {
            try
            {
                CambioDAO daoCambio = new CambioDAO();

                daoCambio.Evento_Contador += DaoCambio_Evento_Contador;
                
                daoCambio.GravarLista(_vm.ListaCambio);
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
        }

        public void Iniciar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg = new StringBuilder();

            _vm.ListaCambio = new List<CambioModel>();
        }

        public bool LerTXT()
        {
            _vm.ErroIndex = 0;

            try
            {
                string[] arrCambio = new string[0];
                string strTexto = File.ReadAllText(_vm.PathArquivoTexto);
                string[] arrTexto = strTexto.Replace("\r\n", "@")
                                            .Split("@");

                List<CambioModel> lstCambio = new List<CambioModel>();

                for (int intCambio = 0; intCambio < arrTexto.Length; intCambio++)
                {
                    arrCambio = arrTexto[intCambio].Split("|");

                    if (arrCambio.Length == 3)
                        lstCambio.Add(new CambioModel()
                        {
                            Numero = lstCambio.Count + 1,
                            Yyyy_Mm_Dd = yyyy_Mm_Dd(arrCambio[0]),
                            Compra = decimal.Parse(arrCambio[1]),
                            Venda = decimal.Parse(arrCambio[2])
                        });
                }

                _vm.ListaCambio = lstCambio;
            }
            catch (Exception excErro)
            {
                _vm.ErroMsg.AppendLine(excErro.Message);
                _vm.ErroIndex = 1;

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion

        #region Eventos

        private void DaoCambio_Evento_Contador(object? sender, EventArgs e)
        {
            Evento_Gravacoes?.Invoke(sender, e);
        }

        #endregion
    }
}