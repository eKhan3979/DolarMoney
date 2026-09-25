using System;
using System.Collections.ObjectModel;
using System.Data;

using MySqlConnector;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.DAO
{
    public class AnaliseDAO: BaseDAO
    {
        #region Construtor

        public AnaliseDAO() { }

        #endregion

        #region Público

        public async Task<AnaliseModel> GetPorInvestimento(int intIdInvestimento)
        {
            AnaliseModel analise = new AnaliseModel();

            try
            {
                string strSql = $"Call u258112148_1.SpDAnalise_GetPorInvestimento({intIdInvestimento});";

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
                            if (reader.Read())
                                analise = new AnaliseModel()
                                {
                                    IdAnalise = ParaInteiro(reader[0]),
                                    DataAnalise = ParaDateTime(reader[1]),
                                    IdInvestimento = ParaInteiro(reader[2]),
                                    IdTendencia = ParaInteiro(reader[3]),
                                    DataCompra = ParaDateTime(reader[4]),
                                    DataVenda = ParaDateTime(reader[5]),
                                    DiasMM1 = ParaInteiro(reader[6]),
                                    DiasMM2 = ParaInteiro(reader[7]),
                                    Expirado = ParaShort(reader[8])
                                };

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

            return analise;
        }

        public async Task<int> Gravar(AnaliseModel analise)
        {
            int intIdAnalise = analise.IdAnalise;

            try
            {
                string strSql = $@"Call u258112148_1.SpDAnalise_Gravar
({analise.IdAnalise},
 {Yyyy_Mm_Dd(analise.DataAnalise)},
 {analise.IdInvestimento},
 {analise.IdTendencia},
 {Yyyy_Mm_Dd(analise.DataCompra)},
 {Yyyy_Mm_Dd(analise.DataVenda)},
 {analise.DiasMM1},
 {analise.DiasMM2});";

                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        comando.CommandType = CommandType.Text;
                        comando.CommandText = strSql;

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                                intIdAnalise = reader.GetInt32(0);

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

            return intIdAnalise;
        }

        public async Task<ObservableCollection<AnaliseDTO>> Lista(int intIdAnalise,
                                                                  int intIdInvestimento,                           
                                                         EnumExpirado eExpirado = EnumExpirado.Todos)
        {
            ObservableCollection<AnaliseDTO> lstAnalises = new ObservableCollection<AnaliseDTO>();

            try
            {
                string strSql = $@"Call u258112148_1.SpDAnalise_Lista(
{intIdAnalise},
{intIdInvestimento},
{eExpirado.GetHashCode()});";

                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        comando.CommandText = strSql;
                        comando.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                lstAnalises.Add(new AnaliseDTO()
                                {
                                    Ticker = ParaString(reader[0]),
                                    DataAnalise = ParaDateTime(reader[1]),
                                    Tendencia = ParaString(reader[2]),
                                    IdAnalise = ParaInteiro(reader[3]),
                                    IdInvestimento = ParaInteiro(reader[4]),
                                    IdTendencia = ParaInteiro(reader[5]),
                                    DataCompra = ParaDateTime(reader[6]),
                                    DataVenda = ParaDateTime(reader[7]),
                                    DiasMM1 = ParaInteiro(reader[8]),
                                    DiasMM2 = ParaInteiro(reader[9]),
                                    Expirado = ParaShort(reader[10])
                                });

                            await reader.CloseAsync();
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

            return lstAnalises;
        }

        public async Task<ObservableCollection<TendenciaAtualDTO>> ListaTendenciasAtual()
        {
            ObservableCollection<TendenciaAtualDTO> lstTendencias = new ObservableCollection<TendenciaAtualDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        comando.CommandText = "Call u258112148_1.SpDAnalise_TendenciasAtuais();";
                        comando.CommandType = CommandType.Text;
                        
                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                lstTendencias.Add(new TendenciaAtualDTO()
                                {
                                    Numero = lstTendencias.Count + 1,
                                    Ticker = ParaString(reader[0]),
                                    Tendencia = ParaString(reader[1]),
                                    IdInvestimento = ParaInteiro(reader[2]),
                                    IdAnalise = ParaInteiro(reader[3]),
                                    DataAnalise = ParaDateTime(reader[4]),
                                    IdTendencia = ParaInteiro(reader[5]),
                                    DataCompra = ParaDateTime(reader[6]),
                                    DataVenda = ParaDateTime(reader[7]),
                                    DiasMM1 = ParaInteiro(reader[8]),
                                    DiasMM2 = ParaInteiro(reader[9]),
                                    Expirado = ParaShort(reader[10])
                                });

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

            return lstTendencias;
        }

        #endregion
    }
}