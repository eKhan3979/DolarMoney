using System;
using System.Collections.Generic;
using System.Data;

using MySqlConnector;

using DolarMoney.Model;

namespace DolarMoney.DAO
{
    public class CambioDAO: BaseDAO
    {
        #region Construtor

        public CambioDAO() { }

        #endregion

        #region Público

        public event EventHandler Evento_Contador;

        public async Task<decimal> Get(string strYyyy_Mm_Dd)
        {
            decimal dcmVenda = 0;

            try
            {
                string strSql = $@"Call u258112148_1.SpDCambio_Get('{strYyyy_Mm_Dd}')";

                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = strSql;
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync()) 
                        {
                            if (await reader.ReadAsync())
                                dcmVenda = reader.GetDecimal(0);

                            await reader.CloseAsync();
                            await reader.DisposeAsync();
                        }

                        await comando.DisposeAsync();
                    }
                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return dcmVenda;
        }

        public async Task<bool> Gravar(MySqlConnection conexao, CambioModel cambio)
        {
            bool boolOk = false;

            try
            {
                using (MySqlCommand comando = conexao.CreateCommand())
                {
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = $@"Call u258112148_1.SpDCambio_Gravar 
('{cambio.Yyyy_Mm_Dd}',
{cambio.Compra.ToString().Replace(",", ".")},
{cambio.Venda.ToString().Replace(",", ".")});";

                    await comando.ExecuteNonQueryAsync();
                    await comando.DisposeAsync();

                    boolOk = true;
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }
        
        public async void GravarLista(List<CambioModel> lstCambio)
        {
            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    if (conexao.State == ConnectionState.Closed)
                        await conexao.OpenAsync();

                    for (int intCambio = 0; intCambio < lstCambio.Count; intCambio++)
                    {
                        await Gravar(conexao, lstCambio[intCambio]);

                        if ((intCambio + 1) % 2 == 0)
                            Evento_Contador?.Invoke(intCambio + 1, new EventArgs());
                    }

                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
        }

        public async Task<List<CambioModel>> Lista(string strYyyy_Mm_Dd_De,
                                                   string strYyyy_Mm_Dd_Ate)
        {
            List<CambioModel> lstCambio = new List<CambioModel>();

            try
            {
                string strSql = $@"Call u258112148_1.SpDCambio_Lista(
'{strYyyy_Mm_Dd_De}',
'{strYyyy_Mm_Dd_Ate}')";

                using (MySqlConnection conexao = GetConexao())
                {
                    if (conexao.State == ConnectionState.Closed)
                        await conexao.OpenAsync();

                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = strSql;
                        comando.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lstCambio.Add(new CambioModel()
                                {
                                    Yyyy_Mm_Dd = reader.GetString(0),
                                    Compra = reader.GetDecimal(1),
                                    Venda = reader.GetDecimal(2)
                                });
                            }

                            await reader.CloseAsync();
                            await reader.DisposeAsync();
                        }

                        await comando.DisposeAsync();
                    }

                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstCambio;
        }

        #endregion
    }
}