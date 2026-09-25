using System;
using System.Collections.ObjectModel;
using System.Text;

using HtmlAgilityPack;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class InvestingRegras: BaseRegras
    {
        #region Variáveis da Classe

        private InvestingViewModel _vm;

        #endregion

        #region Construtor

        public InvestingRegras(ref InvestingViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Público

        public async Task<bool> Gravar()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                string strYyyy_Mm_Dd = _vm.DataImportacao.ToString("yyyy/MM/dd");

                await (new CotacaoDAO()).GravarLista(strYyyy_Mm_Dd, _vm.ListaCotacaoes);

                /*
                for (int intCotacao = 0; intCotacao < _vm.ListaCotacaoes.Count; intCotacao++)
                {
                    CotacaoModel cotacao = new CotacaoModel()
                    {
                        Cotacao = _vm.ListaCotacaoes[intCotacao].Cotacao,
                        IdInvestimento = _vm.ListaCotacaoes[intCotacao].IdInvestimento,
                        DataCotacao = strYyyy_Mm_Dd
                    };

                    await daoCotacao.Gravar(cotacao);
                }
                */

            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);
            }

            return (_vm.ErroIndex == 0);
        }

        public async Task<bool> Inicializar()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg = new System.Text.StringBuilder();

                _vm.DataImportacao = DateTime.Today;
                
                InvestimentoDAO daoInvestimento = new InvestimentoDAO();

                ObservableCollection<IdDescricaoDTO> lstIdInvestimentos = await daoInvestimento.ListaIdTicker(true);

                List<CotacaoInvestingModel> lstCotacoes = new List<CotacaoInvestingModel>();

                foreach (IdDescricaoDTO investimento in lstIdInvestimentos)
                    lstCotacoes.Add(new CotacaoInvestingModel()
                    {
                        Numero = lstCotacoes.Count + 1,
                        IdInvestimento = investimento.Id,
                        Ticker = investimento.Descricao
                    });

                _vm.ListaCotacaoes = lstCotacoes;
                
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);
            }

            return (_vm.ErroIndex == 0);
        }

        public void Importacao()
        {
            if (!string.IsNullOrWhiteSpace(_vm.PathHTML))
            {
                try
                {
                    string strHtml = File.ReadAllText(_vm.PathHTML);

                    HtmlDocument docHTML = new HtmlDocument();

                    docHTML.LoadHtml(strHtml);

                    HtmlNode? nodeTicker = null;
                    HtmlNode? nodeCotacao = null;

                    for (int intNode = 0; intNode < docHTML.DocumentNode.ChildNodes[0].ChildNodes[1].ChildNodes.Count; intNode++)
                    {
                        try
                        {
                            nodeTicker = docHTML.DocumentNode.ChildNodes[0].ChildNodes[1].ChildNodes[intNode].ChildNodes[7];
                            nodeCotacao = docHTML.DocumentNode.ChildNodes[0].ChildNodes[1].ChildNodes[intNode].ChildNodes[11];

                            CotacaoInvestingModel? cotacao = _vm.ListaCotacaoes.FirstOrDefault(t => t.Ticker == nodeTicker.InnerText.Trim());

                            if (cotacao != null)
                                cotacao.CotacaoStr = nodeCotacao.InnerText.Trim();
                        }
                        catch { }
                    }
                }
                catch (Exception excErro)
                {
                    throw excErro;
                }
            }
            else
                throw new Exception("- Selecione o HTML à importar !");
        }

        #endregion
    }
}