using MySqlConnector;

using System.Collections.ObjectModel;
using System.Data;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.DAO
{
    public class OperacaoDAO: BaseDAO
    {
        #region Construtor

        public OperacaoDAO() { }

        #endregion

        #region Público

        public int Gravar(OperacaoModel operacao)
        {
            int intIdOperacao = 0;

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDOperacao_Gravar
({operacao.IdOperacao},
 {operacao.IdInvestimento},
 {Yyyy_Mm_Dd(operacao.DataOperacao)},
'{operacao.CV}',
 {TiraVirgula(operacao.Quantidade)},
 {TiraVirgula(operacao.PrecoUnitario)});";

                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                                intIdOperacao = reader.GetInt32(0);

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

            return intIdOperacao;
        }

        public async Task<int> Insert(OperacaoModel operacao)
        {
            int intIdOperacao = 0;

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDOperacao_Insert(
{operacao.IdOperacao},
{operacao.IdInvestimento},
{Yyyy_Mm_Dd(operacao.DataOperacao)},
'{operacao.CV}',
{TiraVirgula(operacao.Quantidade)},
{TiraVirgula(operacao.Taxa)},
{TiraVirgula(operacao.ValorTotal)},
{TiraVirgula(operacao.PrecoUnitario)});";

                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                                intIdOperacao = reader.GetInt32(0);

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

            return intIdOperacao;
        }

        public async Task<ObservableCollection<OperacaoModel>> Lista(int intIdInvestimento,
                                                                  string strYyyy_Mm_Dd_De,
                                                                  string strYyyy_Mm_Dd_Ate,
                                                                     int intIdTipoInvestimento)
        {
            ObservableCollection<OperacaoModel> lstOperacoes = new ObservableCollection<OperacaoModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand()) 
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDOperacao_Lista(
 {intIdInvestimento},
'{strYyyy_Mm_Dd_De}',
'{strYyyy_Mm_Dd_Ate}',
 {intIdTipoInvestimento});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lstOperacoes.Add(new OperacaoModel()
                                {
                                    IdOperacao = reader.GetInt32(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    DataOperacao = Convert.ToDateTime(reader.GetString(2)),
                                    CV = reader.GetString(3),
                                    Quantidade = reader.GetDecimal(4),
                                    Taxa = reader.GetDecimal(5),
                                    ValorTotal = reader.GetDecimal(6),
                                    PrecoUnitario = reader.GetDecimal(7),
                                    Ticker = reader.GetString(8),
                                    LucroPrejuizo = reader.GetDecimal(9),
                                    LucroPrejuizoPercentual = reader.GetDecimal(9) / reader.GetDecimal(6)
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

            return lstOperacoes;
        }

        public async Task<ObservableCollection<OperacaoInvestimentoDTO>> ListaInvestimento(int intIdInvestimento,
                                                                                        string strYyyy_Mm_Dd_De,
                                                                                        string strYyyy_Mm_Dd_Ate,
                                                                                           int intIdTipoInvestimento)
        {
            ObservableCollection<OperacaoInvestimentoDTO> lstOperacoes = new ObservableCollection<OperacaoInvestimentoDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDOperacao_Lista(
 {intIdInvestimento},
'{strYyyy_Mm_Dd_De}',
'{strYyyy_Mm_Dd_Ate}',
 {intIdTipoInvestimento});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lstOperacoes.Add(new OperacaoInvestimentoDTO()
                                {
                                    IdOperacao = reader.GetInt32(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    DataOperacao = Convert.ToDateTime(reader.GetString(2)),
                                    CV = reader.GetString(3),
                                    Quantidade = reader.GetDecimal(4),
                                    Taxa = reader.GetDecimal(5),
                                    ValorTotal = reader.GetDecimal(6),
                                    PrecoUnitario = reader.GetDecimal(7),
                                    Ticker = reader.GetString(8),
                                    LucroPrejuizo = reader.GetDecimal(9),
                                    LucroPrejuizoPercentual = reader.GetDecimal(9) / reader.GetDecimal(6)
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

            return lstOperacoes;
        }

        public async Task<List<OperacaoCambioDTO>> ListaIR(string strYyyy_Mm_Dd_De, 
                                                           string strYyyy_Mm_Dd_Ate)
        {
            List<OperacaoCambioDTO> lstOperacoes = new List<OperacaoCambioDTO>();

            try
            {
                string strSql = $@"Call u258112148_1.SpDOperacao_ListaIR('{strYyyy_Mm_Dd_De}', '{strYyyy_Mm_Dd_Ate}');";

                using (MySqlConnection conexao = GetConexao())
                {
                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();

                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = strSql;
                        comando.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                lstOperacoes.Add(new OperacaoCambioDTO()
                                {
                                    Numero = lstOperacoes.Count + 1,
                                    DataOperacao = reader.GetString(0),
                                    IdOperacao = reader.GetInt32(1),
                                    IdInvestimento = reader.GetInt32(2),
                                    CV = reader.GetString(3),
                                    Quantidade = reader.GetDecimal(4),
                                    Taxa = reader.GetDecimal(5),
                                    ValorTotal = reader.GetDecimal(6),
                                    PrecoUnitario = reader.GetDecimal(7),
                                    Ticker = reader.GetString(8),
                                    Venda = ParaDecimal(reader[9])
                                });
                            }

                            await reader.CloseAsync();
                            await reader.DisposeAsync();
                        }

                        await comando.DisposeAsync();
                    }

                    await conexao.DisposeAsync();
                    await conexao.CloseAsync();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstOperacoes;
        }

        #endregion
    }
}