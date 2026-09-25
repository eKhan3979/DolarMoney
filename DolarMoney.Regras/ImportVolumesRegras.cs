using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ClosedXML.Excel;

using DolarMoney.DAO;
using DolarMoney.Model.DTO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class ImportVolumesRegras: BaseRegras
    {
        #region Eventos Público

        public event EventHandler Evento_RegistrosGravados;

        #endregion

        #region Variáveis da Classe

        private ImportVolumesViewModel _vm;
        private CotacaoDAO _daoCotacao;

        #endregion

        #region Construtor

        public ImportVolumesRegras(ref ImportVolumesViewModel vm) 
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private void daoCotacao_Evento_RegistroGravado(object? sender, EventArgs e)
        {
            Evento_RegistrosGravados?.Invoke(sender, e);
        }

        private string yyyy_mm_dd(string strMm_Dd_Yyyy)
        {
            return strMm_Dd_Yyyy.Substring(6, 4) + "/" +
                   strMm_Dd_Yyyy.Substring(0, 2) + "/" +
                   strMm_Dd_Yyyy.Substring(3, 2);
        }

        #endregion

        #region Público

        public async Task<bool> Gravar()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                if (_daoCotacao == null)
                {
                    _daoCotacao = new CotacaoDAO();
                    _daoCotacao.Evento_RegistroGravado += daoCotacao_Evento_RegistroGravado;
                }

                if (await _daoCotacao.UpdateVolume(_vm.ListaVolumes))
                {

                }
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        public async Task<bool> Iniciar()
        {
            bool boolOk = false;

            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg = new StringBuilder();

                _vm.ListaInvestimentos = await (new InvestimentoDAO()).ListaIdTicker(true);
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);

                throw excErro;
            }

            return boolOk;
        }

        public bool LerCSV()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                List<CotacaoVolumeDTO> lstVolumes = new List<CotacaoVolumeDTO>();

                using (FileStream fsmCSV = File.OpenRead(_vm.PathCSV))
                {
                    byte[] byteCSV = new byte[fsmCSV.Length];

                    fsmCSV.Read(byteCSV, 0, byteCSV.Length);

                    StringBuilder sbuCSV = new StringBuilder(Encoding.UTF8.GetString(byteCSV));

                    fsmCSV.Close();
                    fsmCSV.Dispose();

                    string[] arrCSV = sbuCSV.ToString().Split('\n');
                    string[] arrColunas = new string[0];

                    for (int intRow = 1; intRow < arrCSV.Length; intRow++)
                    {
                        if (!string.IsNullOrWhiteSpace(arrCSV[intRow]))
                        {
                            arrColunas = arrCSV[intRow].Split(",");

                            lstVolumes.Add(new CotacaoVolumeDTO()
                            {
                                IdInvestimento = _vm.InvestimentoSelecionado.Id,
                                DataCotacao = yyyy_mm_dd(arrColunas[0]),
                                Cotacao = ParaDecimal(arrColunas[1].Replace("$", "").Replace(".", ",")),
                                Volume = int.Parse(arrColunas[2]),
                                Maximo = ParaDecimal(arrColunas[4].Replace("$", "").Replace(".", ",")),
                                Minimo = ParaDecimal(arrColunas[5].Replace("$", "").Replace(".", ","))
                            });
                        }
                    }

                    lstVolumes = lstVolumes.OrderBy(t => t.DataCotacao).ToList();

                    for (int intRow = 0; intRow < lstVolumes.Count; intRow++)
                        lstVolumes[intRow].Numero = intRow + 1;

                    _vm.ListaVolumes = lstVolumes;
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        public bool LerXLSX()
        {
            try
            {
                List<CotacaoVolumeDTO> lstVolumes = new List<CotacaoVolumeDTO>();

                using (XLWorkbook workbook = new XLWorkbook(_vm.PathXLSX))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    int intRow = 0;

                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        if (intRow > 0)
                            lstVolumes.Add(new CotacaoVolumeDTO()
                            {
                                Numero = lstVolumes.Count + 1,
                                IdInvestimento = _vm.InvestimentoSelecionado.Id,
                                DataCotacao = row.Cell(1).GetValue<string>(),
                                Volume = row.Cell(3).GetValue<int>(),
                                Maximo = row.Cell(5).GetValue<decimal>(),
                                Minimo = row.Cell(6).GetValue<decimal>()
                            });

                        intRow++;
                    }

                    workbook.Dispose();
                }

                _vm.ListaVolumes = lstVolumes;
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }
        
        public void Limpar()
        {
            _vm.PathXLSX = "";
            _vm.InvestimentoSelecionado = new IdDescricaoDTO();
            _vm.ListaVolumes = new List<CotacaoVolumeDTO>();
        }

        public bool PodeImportar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if ((_vm.InvestimentoSelecionado == null) ||
                (_vm.InvestimentoSelecionado.Id == 0))
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione a Ação a Importar");
            }
            else
            {
                if ((_vm.ListaVolumes == null) ||
                    (_vm.ListaVolumes.Count == 0))
                {
                    _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                    _vm.ErroMsg.AppendLine("- Não existem registros a importar");
                }
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}