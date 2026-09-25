using System;
using System.Collections.Generic;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class IRRegras: BaseRegras
    {
        #region Variáveis da Classe

        public IRViewModel _vm;

        #endregion

        #region Construtor

        public IRRegras(ref IRViewModel vm)
        {
            _vm  = vm;
        }

        #endregion

        #region Público

        public void Iniciar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg = new StringBuilder();

            try
            {
                _vm.DataInicial = new DateTime(2024, 11, 20);
                _vm.DataFinal = DateTime.Today;
                _vm.ListaOperacoes = new List<OperacaoCambioDTO>();
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
        }

        public async Task<bool> Pesquisar()
        {
            bool boolOk = false;

            try
            {
                _vm.ListaOperacoes = await (new OperacaoDAO()).ListaIR(_vm.DataInicial.ToString("yyyy/MM/dd"),
                                                                         _vm.DataFinal.ToString("yyyy/MM/dd"));

                List<OperacaoCambioDTO> lstEstoques = _vm.ListaOperacoes.OrderBy(t => t.Ticker)
                                                                        .ThenBy(t => t.DataOperacao)
                                                                        .ThenBy(t => t.IdOperacao)
                                                                        .ToArray()
                                                                        .ToList();

                string strYyyy_Mm = "",
                        strTicker = "";
                decimal dcmCusto = 0,
                        dcmEstoque = 0,
                        dcmLucroAcumulado = 0,
                        dcmLucroMensal = 0;

                for (int intOperacao = 0; intOperacao <  lstEstoques.Count; intOperacao++)
                {
                    if (lstEstoques[intOperacao].Ticker != strTicker)
                    {
                        strTicker = lstEstoques[intOperacao].Ticker;
                        dcmEstoque = 0;
                        dcmCusto = 0;
                    }

                    if (lstEstoques[intOperacao].CV == "C")
                    {
                        dcmEstoque += lstEstoques[intOperacao].Quantidade;
                        dcmCusto += lstEstoques[intOperacao].ValorTotal;

                        lstEstoques[intOperacao].EstoqueValor = dcmCusto;
                        lstEstoques[intOperacao].EstoqueAcumulado = dcmEstoque;
                    }
                    else
                    {
                        lstEstoques[intOperacao].Custo = lstEstoques[intOperacao].Quantidade * (dcmCusto / dcmEstoque);
                        lstEstoques[intOperacao].LucroPrejuizo = lstEstoques[intOperacao].ValorTotal - lstEstoques[intOperacao].Custo;

                        dcmEstoque -= lstEstoques[intOperacao].Quantidade;
                        dcmCusto -= lstEstoques[intOperacao].Custo;
                    }
                }

                for (int intOperacao = 0; intOperacao < lstEstoques.Count; intOperacao++)
                {
                    OperacaoCambioDTO? operacao = _vm.ListaOperacoes.FirstOrDefault(t => t.Numero.Equals(lstEstoques[intOperacao].Numero));

                    if (operacao != null)
                    {
                        operacao.Custo = lstEstoques[intOperacao].Custo;
                        operacao.EstoqueAcumulado = lstEstoques[intOperacao].EstoqueAcumulado;
                        operacao.EstoqueValor = lstEstoques[intOperacao].EstoqueValor;
                        operacao.LucroPrejuizo = lstEstoques[intOperacao].LucroPrejuizo;
                    }
                }

                for (int intOperacao = 0; intOperacao < _vm.ListaOperacoes.Count; intOperacao++)
                {
                    if (_vm.ListaOperacoes[intOperacao].DataOperacao.Substring(0, 7) != strYyyy_Mm)
                    {
                        strYyyy_Mm = _vm.ListaOperacoes[intOperacao].DataOperacao.Substring(0, 7);

                        if (intOperacao > 0)
                            _vm.ListaOperacoes[intOperacao - 1].LucroPrejuizoMensal = dcmLucroMensal;

                        if (dcmLucroMensal > 0)
                        {
                            _vm.ListaOperacoes[intOperacao - 1].LucroEmReal = 0.15m * dcmLucroMensal * _vm.ListaOperacoes[intOperacao - 1].Venda.Value;
                            dcmLucroMensal = 0;
                        }
                    }

                    if (_vm.ListaOperacoes[intOperacao].CV == "V")
                    {
                        dcmLucroMensal += _vm.ListaOperacoes[intOperacao].LucroPrejuizo;
                        dcmLucroAcumulado += _vm.ListaOperacoes[intOperacao].LucroPrejuizo;

                        _vm.ListaOperacoes[intOperacao].LucroPrejuizoAcumulado = dcmLucroAcumulado;
                    }
                }

                boolOk = true;
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
