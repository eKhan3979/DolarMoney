using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.InteropServices;

using MySqlConnector;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.DAO
{
    public class InvestimentoDAO: BaseDAO
    {
        #region Construtor

        public InvestimentoDAO() { }

        #endregion

        #region Público

        public InvestimentoModel Get(int intIdInvestimento)
        {
            InvestimentoModel investimento = new InvestimentoModel();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $"Call u258112148_1.SpDInvestimento_Get({intIdInvestimento});";
                        comando.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                                investimento = new InvestimentoModel()
                                {
                                    IdInvestimento = reader.GetInt32(0),
                                    IdTipoInvestimento = reader.GetInt32(1),
                                    Nome = reader.GetString(2),
                                    Ticker = reader.GetString(3),
                                    DataCadastro = reader.GetDateTime(4),
                                    Ativo = reader.GetBoolean(5)
                                };

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

            return investimento;
        }

        public int Gravar(InvestimentoModel investimento)
        {
            int intIdInvestimento = 0;

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = @$"Call u258112148_1.SpDInvestimento_Gravar({investimento.IdInvestimento},
{investimento.IdTipoInvestimento},
'{investimento.Ticker.Trim()}',
'{investimento.Nome.Trim()}',
{Yyyy_Mm_Dd(investimento.DataCadastro)});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                                intIdInvestimento = reader.GetInt32(0);

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

            return intIdInvestimento;
        }

        public async Task<ObservableCollection<InvestimentoModel>> Lista(int intIdTipoInvestimento,
                                                                        bool boolSoAtivos = true)
        {
            ObservableCollection<InvestimentoModel> lstInvestimentos = new ObservableCollection<InvestimentoModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDInvestimento_Lista(
{intIdTipoInvestimento},
{((boolSoAtivos) ? "1" : "Null")});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                lstInvestimentos.Add(new InvestimentoModel()
                                {
                                    IdInvestimento = reader.GetInt32(0),
                                    IdTipoInvestimento = reader.GetInt32(1),
                                    Ticker = reader.GetString(2),
                                    Nome = reader.GetString(3),
                                    DataCadastro = reader.GetDateTime(4),
                                    Ativo = reader.GetBoolean(5)
                                });
                            }

                            reader.CloseAsync();
                            reader.DisposeAsync();
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

            return lstInvestimentos;
        }

        public async Task<ObservableCollection<IdDescricaoDTO>> ListaIdTicker(bool boolSoAtivos = true)
        {
            ObservableCollection<IdDescricaoDTO> lstInvestimentos = new ObservableCollection<IdDescricaoDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDInvestimento_ListaIdTicker(
{((boolSoAtivos) ? "1" : "Null")});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lstInvestimentos.Add(new IdDescricaoDTO()
                                {
                                    Id = reader.GetInt32(0),
                                    Descricao = reader.GetString(1)
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

            return lstInvestimentos;
        }

        #endregion
    }
}