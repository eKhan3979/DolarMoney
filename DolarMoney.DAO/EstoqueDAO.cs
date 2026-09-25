using System;
using System.Collections.ObjectModel;
using System.Data;

using MySqlConnector;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.DAO
{
    public class EstoqueDAO: BaseDAO
    {
        #region Construtor

        public EstoqueDAO() { }

        #endregion

        #region Público

        public ObservableCollection<EstoqueDTO> EstoqueAtual()
        {
            ObservableCollection<EstoqueDTO> lstEstoque = new ObservableCollection<EstoqueDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = "Call u258112148_1.SpDEstoque_Atual();";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                                lstEstoque.Add(new EstoqueDTO()
                                {
                                    Ticker = reader.GetString(0),
                                    Nome = reader.GetString(1),
                                    IdInvestimento = reader.GetInt32(2),
                                    DataOperacao = reader.GetString(3),
                                    Quantidade = reader.GetDecimal(4),
                                    ValorTotal = reader.GetDecimal(5),
                                    CotaAtual = ParaDecimal(reader[6]),
                                    TotalAtual = ParaDecimal(reader[7]),
                                    CustoUnitario = reader.GetDecimal(8),
                                    LucroPrejuizo = reader.GetDecimal(9),
                                    LucroPrejuizoPercentual = reader.GetDecimal(9) / reader.GetDecimal(5)
                                });

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

            return lstEstoque;
        }

        public int Gravar(EstoqueModel estoque)
        {
            int intIdEstoque = 0;

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"u258112148_1.SpDEstoque_Gravar
({estoque.IdEstoque},
 {estoque.IdInvestimento},
'{estoque.DataOperacao}',
 {estoque.Quantidade},
 {estoque.ValorTotal},
 {estoque.LucroPrejuizo});";

                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                                intIdEstoque = reader.GetInt32(0);

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

            return intIdEstoque;
        }

        public ObservableCollection<EstoqueModel> Lista(int intIdInvestimento)
        {
            ObservableCollection<EstoqueModel> lstEstoque = new ObservableCollection<EstoqueModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDEstoque_PorInvestimento({intIdInvestimento});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                lstEstoque.Add(new EstoqueModel()
                                {
                                    IdEstoque = reader.GetInt32(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    DataOperacao = reader.GetString(2),
                                    Quantidade = reader.GetDecimal(3),
                                    ValorTotal = reader.GetDecimal(4),
                                    LucroPrejuizo = reader.GetDecimal(5)
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

            return lstEstoque;
        }

        #endregion
    }
}