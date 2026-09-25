using System;
using System.Collections.ObjectModel;
using System.Data;

using MySqlConnector;

using DolarMoney.Model;

namespace DolarMoney.DAO
{
    public class TipoInvestimentoDAO: BaseDAO
    {
        #region Construtor

        public TipoInvestimentoDAO() { }

        #endregion

        #region Público

        public ObservableCollection<TipoInvestimentoModel> Lista(bool boolAtivos = true)
        {
            ObservableCollection<TipoInvestimentoModel> lstTipoInvestimento = new ObservableCollection<TipoInvestimentoModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        comando.CommandText = $"Call u258112148_1.SpDTipoInvestimento_Lista({((boolAtivos) ? "1" : "Null")})";
                        comando.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lstTipoInvestimento.Add(new TipoInvestimentoModel()
                                {
                                    IdTipoInvestimento = reader.GetInt32(0),
                                    Tipo = reader.GetString(1),
                                    Ativo = reader.GetBoolean(2)
                                });
                            }

                            reader.Close();
                        }

                        comando.Dispose();
                    }

                    conexao.Close();
                    conexao.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstTipoInvestimento;
        }

        #endregion
    }
}
