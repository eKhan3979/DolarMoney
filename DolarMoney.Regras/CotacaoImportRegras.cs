using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

using DolarMoney.DAO;
using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class CotacaoImportRegras: BaseRegras
    {
        #region Variáveis da Classe

        private CotacaoImportViewModel _vm;
        private CotacaoDAO _daoCotacao;

        #endregion

        #region Construtor

        public CotacaoImportRegras(ref CotacaoImportViewModel vm)
        {
            _vm = vm;

            _daoCotacao = new CotacaoDAO();
            _daoCotacao.Evento_RegistroGravado += DaoCotacao_Evento_RegistroGravado;
        }

        #endregion

        #region Eventos

        public event EventHandler Evento_ImportacaoFinalizada;

        private void DaoCotacao_Evento_RegistroGravado(object? sender, EventArgs e)
        {
            try { _vm.RegistrosGravados = (int)sender; }
            catch { }
        }

        #endregion

        #region Público

        public async Task<bool> Gravar()
        {
            try
            {
                _vm.ErroMsg.Clear();
                _vm.ErroIndex = 0;

                _vm.RegistrosAGravar = _vm.ListaCotacoes.Count;
                _vm.RegistrosGravados = 0;

                await _daoCotacao.GravarDTO(_vm.ListaCotacoes, _vm.IdInvestimentoImport);

                _vm.ListaCotacoes = new ObservableCollection<CotacaoImportDTO>();

                Evento_ImportacaoFinalizada?.Invoke("OK", new EventArgs());
            }
            catch (Exception excErro)
            {
                _vm.ErroMsg.AppendLine(excErro.Message);
                _vm.ErroIndex = 1;

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        public async Task<bool> Iniciar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg = new StringBuilder();

            try
            {
                _vm.ListaCotacoes = new ObservableCollection<CotacaoImportDTO>();
                _vm.ListaInvestimentos = await (new InvestimentoDAO()).Lista(enumTipoInvestimento.Acoes.GetHashCode());
            }
            catch (Exception excErro)
            {
                _vm.ErroMsg.AppendLine(excErro.Message);
                _vm.ErroIndex = 1;

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        public bool LerJSON()
        {
            if (!string.IsNullOrWhiteSpace(_vm.PathImport))
            {
                try
                {
                    _vm.ErroIndex = 0;
                    _vm.ErroMsg.Clear();

                    string strJson = File.ReadAllText(_vm.PathImport);

                    if (!string.IsNullOrWhiteSpace(strJson))
                    {
                        ObservableCollection<CotacaoImportDTO> lstCotacoes = JsonSerializer.Deserialize<ObservableCollection<CotacaoImportDTO>>(strJson);

                        for (int intCotacao = 0; intCotacao < lstCotacoes.Count; intCotacao++)
                            lstCotacoes[intCotacao].Numero = intCotacao + 1;

                        _vm.ListaCotacoes = lstCotacoes;
                    }
                    else
                        _vm.ListaCotacoes = new ObservableCollection<CotacaoImportDTO>();
                }
                catch (Exception excErro)
                {
                    _vm.ErroIndex = 1;
                    _vm.ErroMsg.AppendLine(excErro.Message);

                    throw excErro;
                }
            }
            else
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione o Path do arquivo JSON !");
            }

            return (_vm.ErroIndex == 0);
        }

        public bool PodeGravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if (_vm.IdInvestimentoImport == 0)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione Ação");
            }
            if (string.IsNullOrWhiteSpace(_vm.PathImport))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Indique o Path do arquivo JSON");
            }
            if (_vm.ListaCotacoes.Count == 0)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 3 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Não existem Cotações à gravar");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}
