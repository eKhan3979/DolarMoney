using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class CotacaoDoDiaRegras: BaseRegras
    {
        #region Variáveis da Classe

        private CotacaoDoDiaViewModel _vm;

        #endregion

        #region Construtor

        public CotacaoDoDiaRegras(ref CotacaoDoDiaViewModel vm) 
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private string getTicker(int intIdInvestimento)
        {
            string strTicker = "";

            IdDescricaoDTO? investimento = _vm.ListaInvestimentos.FirstOrDefault(t => t.Id.Equals(intIdInvestimento));

            if (investimento != null)
                strTicker = investimento.Descricao;

            return strTicker;
        }

        #endregion

        #region Público

        public void CotacaoSelecionada(CotacaoDoDiaDTO cotacao)
        {
            _vm.CotacaoEdit = new CotacaoModel()
            {
                IdCotacao = cotacao.IdCotacao,
                IdInvestimento = cotacao.IdInvestimento,
                Cotacao = cotacao.Cotacao,
                DataCotacao = cotacao.DataCotacao,
                Numero = cotacao.Numero
            };
        }

        public void CotacaoSelecionada(CotacaoPercentualDTO cotacao, string strDd_Mm_Yyyy)
        {
            _vm.CotacaoEdit = new CotacaoModel()
            {
                IdCotacao = ((cotacao.IdCotacao != null) ? cotacao.IdCotacao.Value : 0),
                IdInvestimento = cotacao.IdInvestimento,
                Cotacao = cotacao.Cotacao,
                DataCotacao = strDd_Mm_Yyyy,
                Numero = cotacao.Numero
            };
        }

        public void CotacaoSelecionadaPorInvestimento()
        {
            int intIdInvestimento = _vm.CotacaoEdit.IdInvestimento;

            CotacaoDoDiaDTO cotacao = _vm.ListaCotacaoDoDia.FirstOrDefault(t => t.IdInvestimento.Equals(intIdInvestimento));

            if (cotacao != null)
                CotacaoSelecionada(cotacao);
        }

        public async Task<bool> Gravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            try
            {
                CotacaoModel cotacao = new CotacaoModel()
                {
                    IdCotacao = _vm.CotacaoEdit.IdCotacao,
                    DataCotacao = _vm.DataCotacao.ToString("yyyy/MM/dd"),
                    IdInvestimento = _vm.CotacaoEdit.IdInvestimento,
                    Numero = _vm.CotacaoEdit.Numero,
                    Cotacao = _vm.CotacaoEdit.Cotacao
                };

                cotacao.IdCotacao = await (new CotacaoDAO()).Gravar(cotacao);

                var atualizar = _vm.ListaCotacaoDoDia.FirstOrDefault(t => t.IdInvestimento.Equals(cotacao.IdInvestimento));

                if (atualizar != null)
                    atualizar.Cotacao = cotacao.Cotacao;
                else
                {
                    List<CotacaoDoDiaDTO> lstCotacoes = new List<CotacaoDoDiaDTO>();

                    lstCotacoes.Add(new CotacaoDoDiaDTO()
                    {
                        IdCotacao = cotacao.IdCotacao,
                        IdInvestimento = cotacao.IdInvestimento,
                        Cotacao = cotacao.Cotacao,
                        DataCotacao = cotacao.DataCotacao,
                        Ticker = getTicker(cotacao.IdInvestimento),
                    });

                    foreach (CotacaoDoDiaDTO cota in _vm.ListaCotacaoDoDia)
                        lstCotacoes.Add(new CotacaoDoDiaDTO()
                        {
                            IdCotacao = cota.IdCotacao,
                            Cotacao = cota.Cotacao,
                            DataCotacao = cota.DataCotacao,
                            IdInvestimento = cota.IdInvestimento,
                            Ticker = cota.Ticker,
                            Numero = cota.Numero
                        });

                    _vm.ListaCotacaoDoDia = lstCotacoes.OrderBy(t => t.Ticker).ToList();

                    for (int intCotacao = 0; intCotacao < _vm.ListaCotacaoDoDia.Count; intCotacao++)
                        _vm.ListaCotacaoDoDia[intCotacao].Numero = intCotacao + 1;
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
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg = new StringBuilder();

                _vm.DataCotacao = DateTime.Today;
                _vm.CotacaoEdit = new CotacaoModel();

                await PesquisaComparativa();

                _vm.ListaInvestimentos = await (new InvestimentoDAO()).ListaIdTicker();
            }
            catch (Exception excErro)
            {
                _vm.ErroMsg.AppendLine(excErro.Message);
                _vm.ErroIndex = 1;

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        public void Limpar()
        {
            _vm.CotacaoEdit = new CotacaoModel();
        }

        public async Task<bool> Pesquisar()
        {
            bool boolOk = false;

            try
            {
                _vm.ListaCotacaoDoDia = await (new CotacaoDAO()).ListaCotacaoDoDia(Dd_Mm_Yyyy(_vm.DataCotacao));

                List<IdDescricaoDTO> lstInvestimentos = new List<IdDescricaoDTO>();

                foreach (CotacaoDoDiaDTO cotacao in _vm.ListaCotacaoDoDia)
                    lstInvestimentos.Add(new IdDescricaoDTO()
                    {
                        Id = cotacao.IdInvestimento,
                        Descricao = cotacao.Ticker
                    });

                _vm.ListaInvestimentos = new ObservableCollection<IdDescricaoDTO>(lstInvestimentos);

                _vm.DataPesquisa = _vm.DataCotacao;

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<bool> PesquisaComparativa()
        {
            bool boolOk = false;

            try
            {
                List<CotacaoPercentualDTO> lstCotacoes = await (new CotacaoDAO()).ListaCotacaoComparativa(Dd_Mm_Yyyy(_vm.DataCotacao));

                decimal dcmDiferenca = 0;

                int intCotacao = 1;

                foreach (CotacaoPercentualDTO cotacao in lstCotacoes)
                {
                    cotacao.Numero = intCotacao;
                    cotacao.CotacaoStr = base.ParaMoeda(cotacao.Cotacao);
                    cotacao.CotacaoAnteriorStr = base.ParaMoeda(cotacao.CotacaoAnterior);

                    if ((cotacao.Cotacao != null) && ( cotacao.CotacaoAnterior != null))
                        dcmDiferenca = cotacao.Cotacao.Value - cotacao.CotacaoAnterior.Value;

                    cotacao.Percentual = (decimal)((dcmDiferenca * 100) / cotacao.CotacaoAnterior);
                    if (cotacao.Percentual != null)
                        cotacao.PercentualStr = cotacao.Percentual.Value.ToString("#,##0.#0") + "%";
                    cotacao.ColorStr = ((cotacao.Percentual >= 0) ? "#FF0000FF" : "#FFFF0000");
                    intCotacao++;
                }

                List<IdDescricaoDTO> lstInvestimentos = new List<IdDescricaoDTO>();

                foreach (CotacaoDoDiaDTO cotacao in _vm.ListaCotacaoDoDia)
                    lstInvestimentos.Add(new IdDescricaoDTO()
                    {
                        Id = cotacao.IdInvestimento,
                        Descricao = cotacao.Ticker
                    });

                _vm.ListaCotacoes = lstCotacoes;

                _vm.ListaInvestimentos = new ObservableCollection<IdDescricaoDTO>(lstInvestimentos);

                _vm.DataPesquisa = _vm.DataCotacao;

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public bool PodeGravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if (_vm.CotacaoEdit.IdInvestimento <= 0)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione o Investimento");
            }
            if (_vm.DataCotacao.Year < 2024)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- São permitidas cotações apenas à partir de 2024");
            }
            if ((_vm.CotacaoEdit.Cotacao == null) ||
                (_vm.CotacaoEdit.Cotacao.Value <= 0))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 3 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Preencha o Valor da Cotação");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}