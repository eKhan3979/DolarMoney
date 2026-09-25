using System.Collections.ObjectModel;
using System.Data;

using MySqlConnector;

using DolarMoney.Model;
using DolarMoney.Model.DTO;

namespace DolarMoney.DAO
{    
    public class CotacaoDAO: BaseDAO
    {
        #region Eventos

        public event EventHandler Evento_RegistroGravado;

        #endregion

        #region Construtor

        public CotacaoDAO() { }

        #endregion

        #region Private

        private string decParaString(decimal decValor, int intLen)
        {                      //12345
            string strEspacos = "     ",
                   strValor = decValor.ToString("0.#0").Replace(",", ".");

            if (strValor.Length < intLen)
                strValor = strEspacos.Substring(0, intLen - strValor.Length) + strValor;

            return strValor;
        }

        private string intParaString(int intValor, int intLen)
        {                      //1234567890 
            string strEspacos = "          ",
                   strValor = intValor.ToString();

            if (strValor.Length < intLen)
                strValor = strValor + strEspacos.Substring(0, intLen - strValor.Length);

            return strValor;
        }

        #endregion

        #region Público

        public async Task<int> Gravar(CotacaoModel cotacao)
        {
            int intIdCotacao = 0;

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDCotacao_Gravar
({cotacao.IdCotacao},
 {cotacao.IdInvestimento},
'{cotacao.DataCotacao}',
 {cotacao.Cotacao.Value.ToString().Replace(",", ".")});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                                intIdCotacao = reader.GetInt32(0);

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

            return intIdCotacao;
        }

        public async Task<bool> GravarDTO(ObservableCollection<CotacaoImportDTO> lstCotacoes, int intIdInvestimento)
        {
            bool boolOk = false;

            try
            {
                int intGravacoes = 0;

                using (MySqlConnection conexao = GetConexao())
                {
                    int intIdCotacao = 0;

                    for (int intCotacao = 0; intCotacao < lstCotacoes.Count; intCotacao++)
                    {
                        using (MySqlCommand comando = conexao.CreateCommand())
                        {
                            comando.CommandText = $@"Call u258112148_1.SpDCotacao_Gravar
(0,
{intIdInvestimento},
{Yyyy_Mm_Dd(lstCotacoes[intCotacao].Dd_Mm_Yyyy_Cotacao)},
{lstCotacoes[intCotacao].ValorCotacao.ToString().Replace(".", "").Replace(",", ".")});";
                            comando.CommandType = CommandType.Text;

                            if (conexao.State == ConnectionState.Closed)
                                await conexao.OpenAsync();

                            using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                    intIdCotacao = reader.GetInt32(0);
                                //lstCotacoes[intCotacao].IdCotacao = reader.GetInt32(0);

                                await reader.CloseAsync();
                                await reader.DisposeAsync();
                            }

                            await comando.DisposeAsync();

                            intGravacoes++;

                            Evento_RegistroGravado?.Invoke(intGravacoes, new EventArgs());
                        }
                    }

                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<bool> GravarLista(string strYyyy_Mm_Dd, List<CotacaoInvestingModel> lstCotacoes)
        {
            bool boolOk = false;

            try
            {
                string strIdInvestimento = "",
                       strCotacao = "",
                       strSql = "";

                using (MySqlConnection conexao = GetConexao())
                {
                    for (int intCotacao = 0; intCotacao < lstCotacoes.Count; intCotacao++)
                    {
                        if (lstCotacoes[intCotacao].Cotacao > 0)
                        {
                            strIdInvestimento += intParaString(lstCotacoes[intCotacao].IdInvestimento, 5);
                            strCotacao += decParaString(lstCotacoes[intCotacao].Cotacao, 10);

                            if (strIdInvestimento.Length == 250)
                            {
                                using (MySqlCommand comando = conexao.CreateCommand())
                                {
                                    if (conexao.State == ConnectionState.Closed)
                                        await conexao.OpenAsync();

                                    strSql = $"Call u258112148_1.SpDCotacao_GravarLista('{strYyyy_Mm_Dd}', '{strIdInvestimento}', '{strCotacao}');";

                                    comando.CommandText = strSql;
                                    comando.CommandType = CommandType.Text;

                                    await comando.ExecuteNonQueryAsync();
                                    await comando.DisposeAsync();
                                }

                                strIdInvestimento = "";
                                strCotacao = "";
                            }
                        }
                    }

                    if (strIdInvestimento.Length > 0)
                    {
                        using (MySqlCommand comando = conexao.CreateCommand())
                        {
                            if (conexao.State == ConnectionState.Closed)
                                await conexao.OpenAsync();

                            strSql = $"Call u258112148_1.SpDCotacao_GravarLista('{strYyyy_Mm_Dd}', '{strIdInvestimento}', '{strCotacao}');";

                            comando.CommandText = strSql;
                            comando.CommandType = CommandType.Text;

                            await comando.ExecuteNonQueryAsync();
                            await comando.DisposeAsync();
                        }
                    }

                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<List<CotacaoPercentualDTO>> ListaCotacaoComparativa(string strDd_Mm_Yyyy)
        {
            List<CotacaoPercentualDTO> lstCotacoes = new List<CotacaoPercentualDTO>();

            try
            {
                string strYyyy_Mm_Dd = strDd_Mm_Yyyy.Substring(6, 4) + "/" +
                                       strDd_Mm_Yyyy.Substring(3, 2) + "/" +
                                       strDd_Mm_Yyyy.Substring(0, 2),
                       strSql = $"Call u258112148_1.SpDCotacao_Comparativa('{strYyyy_Mm_Dd}');";

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
                            {
                                lstCotacoes.Add(new CotacaoPercentualDTO()
                                {
                                    Ticker = reader.GetString(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    IdCotacao = base.ParaInteiroNull(reader[2]),
                                    Cotacao = base.ParaDecimalNull(reader[3]),
                                    CotacaoAnterior = base.ParaDecimalNull(reader[4])
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

            return lstCotacoes;
        }

        public async Task<List<CotacaoDoDiaDTO>> ListaCotacaoDoDia(string strDd_Mm_Yyyy)
        {
            List<CotacaoDoDiaDTO> lstCotacaoDoDia = new List<CotacaoDoDiaDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $"Call u258112148_1.SpDCotacaoDoDia_Lista({Yyyy_Mm_Dd(strDd_Mm_Yyyy)});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                lstCotacaoDoDia.Add(new CotacaoDoDiaDTO()
                                {
                                    Numero = lstCotacaoDoDia.Count + 1,
                                    Ticker = reader.GetString(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    IdCotacao = ParaInteiro(reader[2]),
                                    Cotacao = ParaDecimal(reader[3])
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

            return lstCotacaoDoDia;
        }

        public async Task<ObservableCollection<CotacaoGraficoDTO>> ListaCotacoesGrafico(int intIdInvestimento,
                                                                                     string strYyyy_Mm_Dd_De,
                                                                                     string strYyyy_Mm_Dd_Ate)
        {
            ObservableCollection<CotacaoGraficoDTO> lstCotacaoGraficos = new ObservableCollection<CotacaoGraficoDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $"Call u258112148_1.SpDCotacao_Graficos({intIdInvestimento}, '{strYyyy_Mm_Dd_De}', '{strYyyy_Mm_Dd_Ate}');";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                                lstCotacaoGraficos.Add(new CotacaoGraficoDTO()
                                {
                                    Numero = lstCotacaoGraficos.Count + 1,
                                    DataCotacao = ParaDateTime(Dd_Mm_Yyyy(reader.GetString(0))),
                                    ValorCotacao = ParaDecimalZero(reader[1])
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

            return lstCotacaoGraficos;
        }

        public async Task<ObservableCollection<CotacaoModel>> ListaIdPeriodo(CotacaoPesquisarDTO pesquisar)
        {
            ObservableCollection<CotacaoModel> lstCotacoes = new ObservableCollection<CotacaoModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDCotacao_IdPeriodo(
{pesquisar.IdInvestimentoSelecionado},
{Yyyy_Mm_Dd(pesquisar.PeriodoDe)},
{Yyyy_Mm_Dd(pesquisar.PeriodoAte)});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            await conexao.OpenAsync();

                        using (MySqlDataReader reader = await comando.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lstCotacoes.Add(new CotacaoModel()
                                {
                                    IdCotacao = reader.GetInt32(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    DataCotacao = reader.GetString(2),
                                    Cotacao = reader.GetDecimal(3),
                                    Numero = lstCotacoes.Count + 1
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

            return lstCotacoes;
        }

        public ObservableCollection<IdDescricaoDTO> ListaIdTicker(bool b0oolSoAtivos = true)
        {
            ObservableCollection<IdDescricaoDTO> lstIdTicker = new ObservableCollection<IdDescricaoDTO>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                                lstIdTicker.Add(new IdDescricaoDTO()
                                {
                                    Id = reader.GetInt32(0),
                                    Descricao = reader.GetString(1)
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

            return lstIdTicker;
        }

        public ObservableCollection<CotacaoModel> ListaPorInvestimento(int intIdInvestimento)
        {
            ObservableCollection<CotacaoModel> lstCotacoes = new ObservableCollection<CotacaoModel>();

            try
            {
                using (MySqlConnection conexao = GetConexao())
                {
                    using (MySqlCommand comando = conexao.CreateCommand())
                    {
                        comando.CommandText = $@"Call u258112148_1.SpDCotacao_PorInvestimento({intIdInvestimento});";
                        comando.CommandType = CommandType.Text;

                        if (conexao.State == ConnectionState.Closed)
                            conexao.Open();

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lstCotacoes.Add(new CotacaoModel()
                                {
                                    IdCotacao = reader.GetInt32(0),
                                    IdInvestimento = reader.GetInt32(1),
                                    DataCotacao = reader.GetString(2),
                                    Cotacao = reader.GetDecimal(3)
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

            return lstCotacoes;
        }

        public async Task<bool> UpdateVolume(List<CotacaoVolumeDTO> lstVolumes)
        {
            bool boolOk = false;

            try
            {
                string strSql = "";

                using (MySqlConnection conexao = GetConexao())
                {
                    if (conexao.State == ConnectionState.Closed)
                        await conexao.OpenAsync();

                    for (int intVolume = 0; intVolume < lstVolumes.Count; intVolume++)
                    {
                        strSql = $@"Call u258112148_1.SpDCotacao_UpdateVolume
({lstVolumes[intVolume].IdInvestimento},
'{lstVolumes[intVolume].DataCotacao}',
{TiraVirgula(lstVolumes[intVolume].Cotacao)},
{TiraVirgula(lstVolumes[intVolume].Volume)},
{TiraVirgula(lstVolumes[intVolume].Maximo)},
{TiraVirgula(lstVolumes[intVolume].Minimo)});";

                        using (MySqlCommand comando = conexao.CreateCommand())
                        {
                            comando.CommandText = strSql;
                            comando.CommandType = CommandType.Text;

                            await comando.ExecuteNonQueryAsync();
                            await comando.DisposeAsync();
                        }

                        if ((intVolume + 1) % 10 == 0)
                            Evento_RegistroGravado?.Invoke(intVolume + 1, new EventArgs());
                    }

                    await conexao.CloseAsync();
                    await conexao.DisposeAsync();
                }

                Evento_RegistroGravado?.Invoke(lstVolumes.Count, new EventArgs());
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        #endregion
    }
}