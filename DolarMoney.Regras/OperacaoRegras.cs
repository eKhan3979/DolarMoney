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
    public class OperacaoRegras: BaseRegras
    {
        #region Variáveis da Classe

        private OperacaoViewModel _vm;

        #endregion

        #region Construtor

        public OperacaoRegras(ref OperacaoViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private string getTicker(int intIdInvestimento)
        {
            var investimento = _vm.ListaInvestimentos.FirstOrDefault(t => t.IdInvestimento.Equals(intIdInvestimento));

            if (investimento != null)
                return investimento.Ticker;
            else
                return "";
        }

        #endregion

        #region Público

        public void CalcularResumo()
        {
            _vm.ResumoPesquisa.Lucro = _vm.ListaOperacoes.Where(t => t.LucroPrejuizo > 0)
                                                         .Sum(t => t.LucroPrejuizo);
            _vm.ResumoPesquisa.Prejuizo = _vm.ListaOperacoes.Where(t => t.LucroPrejuizo < 0)
                                                            .Sum(t => t.LucroPrejuizo);
            _vm.ResumoPesquisa.Liquido = _vm.ResumoPesquisa.Lucro +
                                         _vm.ResumoPesquisa.Prejuizo;

            _vm.ResumoPesquisa.ValorEstoque = _vm.ListaEstoques.Sum(t => ((t.CotaAtual != null) ? t.CotaAtual.Value * t.Quantidade : 0));
            _vm.ResumoPesquisa.Estoque = _vm.ListaEstoques.Sum(t => t.LucroPrejuizo);
            _vm.ResumoPesquisa.Total = _vm.ResumoPesquisa.Liquido + _vm.ResumoPesquisa.Estoque;
            if (_vm.ResumoPesquisa.ValorEstoque > 0)
                _vm.ResumoPesquisa.Percentual = _vm.ResumoPesquisa.Estoque / _vm.ResumoPesquisa.ValorEstoque;
            else
                _vm.ResumoPesquisa.Percentual = 0;
        }

        public void CarregarInvestimentos()
        {
            if (_vm != null) //&& (_vm.Iniciado))
            {
                if (_vm.Pesquisar.IdTipoInvestimento > 0)
                {
                    var lista = _vm.ListaInvestimentos.Where(t => t.IdTipoInvestimento.Equals(_vm.Pesquisar.IdTipoInvestimento));

                    lista.ToList().Sort((a, b) => a.Ticker.CompareTo(b.Ticker));

                    _vm.ListaInvestimentosExibir = new ObservableCollection<InvestimentoModel>(lista);

                    if (_vm.Pesquisar.IdTipoInvestimento <= 0)
                    {
                        _vm.Pesquisar.TipoInvestimento = "";
                    }
                    else
                    {
                        if (_vm.ListaInvestimentosExibir.Count == 1)
                            _vm.Pesquisar.IdInvestimento = _vm.ListaInvestimentosExibir[0].IdInvestimento;

                        TipoInvestimentoModel? tp = _vm.ListaTipoInvestimento.FirstOrDefault(t => t.IdTipoInvestimento.Equals(_vm.Pesquisar.IdTipoInvestimento));

                        _vm.Pesquisar.TipoInvestimento = tp?.Tipo;
                    };
                }
                else
                {
                    _vm.ListaInvestimentosExibir = new ObservableCollection<InvestimentoModel>();
                    _vm.Pesquisar.TipoInvestimento = "";
                }
            }
        }

        public async Task<bool> CarregarOperacoesInvestimento()
        {
            bool boolOk = false;

            try
            {
                var lista = await (new OperacaoDAO()).ListaInvestimento(_vm.InvestimentoSelecionado.IdInvestimento,
                                                                        _vm.Pesquisar.PeriodoDe.ToString("yyyy/MM/dd"),
                                                                        _vm.Pesquisar.PeriodoAte.ToString("yyyy/MM/dd"),
                                                                         enumTipoInvestimento.Acoes.GetHashCode());

                decimal dcmCusto = 0,
                        dcmQuantidade = 0,
                        dcmPrecoUnitarioAtual = _vm.InvestimentoSelecionado.TotalAtual.Value / 
                                                _vm.InvestimentoSelecionado.Quantidade;

                for (int intNumero = 0; intNumero < lista.Count; intNumero++)
                {
                    lista[intNumero].Numero = intNumero + 1;

                    if (lista[intNumero].CV == "C")
                    {
                        dcmCusto += lista[intNumero].PrecoUnitario *
                                    lista[intNumero].Quantidade +
                                    lista[intNumero].Taxa;

                        dcmQuantidade += lista[intNumero].Quantidade;
                    }
                    else
                    {
                        dcmCusto = dcmCusto * ((dcmQuantidade - lista[intNumero].Quantidade) / dcmQuantidade);

                        dcmQuantidade -= lista[intNumero].Quantidade;
                    }

                    lista[intNumero].CustoEstoque = dcmCusto;
                    lista[intNumero].QtdeEstoque = dcmQuantidade;
                    lista[intNumero].ValorAtual = lista[intNumero].Quantidade * dcmPrecoUnitarioAtual;

                    if (lista[intNumero].CV == "C")
                        lista[intNumero].Valorizacao = lista[intNumero].ValorAtual - (lista[intNumero].PrecoUnitario *
                                                                                      lista[intNumero].Quantidade +
                                                                                      lista[intNumero].Taxa);
                }

                _vm.ListaOperacoesInvestimentos = lista;

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<bool> Gravar()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                int intIdOperacao = await (new OperacaoDAO()).Insert(_vm.Cadastro);

                if (_vm.Cadastro.IdOperacao == 0)
                {
                    _vm.ListaOperacoes.Add(new OperacaoModel()
                    {
                        IdOperacao = intIdOperacao,
                        CV = _vm.Cadastro.CV,
                        DataOperacao = _vm.Cadastro.DataOperacao,
                        IdInvestimento = _vm.Cadastro.IdInvestimento,
                        PrecoUnitario = _vm.Cadastro.PrecoUnitario,
                        Quantidade = _vm.Cadastro.Quantidade,
                        Taxa = _vm.Cadastro.Taxa,
                        Ticker = getTicker(_vm.Cadastro.IdInvestimento),
                        ValorTotal = _vm.Cadastro.ValorTotal,
                        Numero = _vm.ListaOperacoes.Count + 1
                    });
                }
                else
                {

                }

                Limpar();
            }
            catch (Exception excErro)
            {
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

                _vm.Cadastro = new OperacaoModel()
                {
                    DataOperacao = DateTime.Today
                };

                _vm.ListaCadCV = new ObservableCollection<CodigoDescricaoDTO>()
                {
                    new CodigoDescricaoDTO() { Codigo = "C", Descricao = "Compra" },
                    new CodigoDescricaoDTO() { Codigo = "V", Descricao = "Venda" }
                };

                _vm.ListaTipoInvestimento = (new TipoInvestimentoDAO()).Lista();
                _vm.ListaInvestimentos = await (new InvestimentoDAO()).Lista(enumTipoInvestimento.Acoes.GetHashCode(), true);
                
                _vm.Pesquisar = new OperacaoPesquisarDTO()
                {
                    PeriodoDe = new DateTime(2024, 1, 1),
                    PeriodoAte = DateTime.Today
                };

                if (_vm.ListaTipoInvestimento.Count == 1)
                    _vm.Pesquisar.IdTipoInvestimento = _vm.ListaTipoInvestimento[0].IdTipoInvestimento;

                _vm.ResumoPesquisa = new ResumoDTO();

                _vm.Iniciado = true;
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
            DateTime dtmCadastro = _vm.Cadastro.DataOperacao;

            _vm.Cadastro = new OperacaoModel()
            {
                DataOperacao = dtmCadastro,
                Taxa = (decimal)1.5
            };
        }

        public async Task<ObservableCollection<OperacaoModel>> Pesquisar()
        {
            ObservableCollection<OperacaoModel> lstOperacoes = new ObservableCollection<OperacaoModel>();

            try

            {
                lstOperacoes = await (new OperacaoDAO()).Lista(_vm.Pesquisar.IdInvestimento,
                                                               Yyyy_Mm_Dd(_vm.Pesquisar.PeriodoDe),
                                                               Yyyy_Mm_Dd(_vm.Pesquisar.PeriodoAte),
                                                               _vm.Pesquisar.IdTipoInvestimento);

                for (int intOperacao = 0; intOperacao < lstOperacoes.Count; intOperacao++)
                    lstOperacoes[intOperacao].Numero = intOperacao + 1;

                _vm.ListaOperacoes = lstOperacoes;

                var lstEstoques = (new EstoqueDAO()).EstoqueAtual();

                for (int intEstoque = 0; intEstoque < lstEstoques.Count; intEstoque++)
                    lstEstoques[intEstoque].Numero = intEstoque + 1;

                _vm.ListaEstoques = lstEstoques;

                CalcularResumo();
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstOperacoes;
        }

        public bool PodeGravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if (_vm.Cadastro.IdInvestimento == 0)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione um Investimento");
            }
            if (string.IsNullOrWhiteSpace(_vm.Cadastro.CV))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Indique Compra ou Venda");
            }
            if (_vm.Cadastro.Quantidade == 0)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 3 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Digite a Quantidade");
            }
            if ((_vm.Cadastro.DataOperacao == null) ||
                (Yyyy_Mm_Dd(_vm.Cadastro.DataOperacao).CompareTo("2024/01/01") < 0))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 4 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Data Inválida (deve ser > 01/01/2024");
            }
            if (_vm.Cadastro.ValorTotal <= 0)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 5 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Digite o Valor Total da Operação");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}