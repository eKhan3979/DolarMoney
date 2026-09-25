using System;
using System.Collections.ObjectModel;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;
using System.Data;

namespace DolarMoney.Regras
{
    public class IFRRegras : BaseRegras
    {
        #region Variáveis da Classe

        private IFRViewModel _vm;

        #endregion

        #region Construtor

        public IFRRegras(ref IFRViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private decimal? mediaDia(MACDViewModel vm, int intDia, int intDiasMM)
        {
            decimal? dcmTotal = 0;

            for (int intD = intDia - intDiasMM; intD < intDia; intD++)
                dcmTotal += ((vm.ListaCotacoes[intD].ValorCotacao != null) ? vm.ListaCotacoes[intD].ValorCotacao.Value : 0);

            return ((dcmTotal != null) ? dcmTotal.Value / (decimal)intDiasMM : null);
        }

        private decimal media(ref List<CotacaoIFRDTO> lstIFR, int intDiaMedia)
        {
            try
            {
                decimal dcmGanhos = 0,
                        dcmPerdas = 0;

                for (int intDia = intDiaMedia - _vm.MMDias + 1; intDia < (intDiaMedia + 1); intDia++)
                {
                    dcmGanhos += lstIFR[intDia].Ganho;
                    dcmPerdas += lstIFR[intDia].Perda;
                }

                return dcmGanhos / ((dcmPerdas != 0) ? dcmPerdas : 1);
            }
            catch 
            {
                return 0;
            }
        }

        private decimal naoNull(decimal? dcmValor)
        {
            return ((dcmValor != null) ? dcmValor.Value : 0);
        }

        #endregion

        #region Público

        public void CalcularIFR()
        {
            try
            {
                decimal dcmDelta = 0,
                        dcmGanho = 0,
                        dcmPerda = 0;

                List<CotacaoIFRDTO> lstIFR = new List<CotacaoIFRDTO>();

                /*
                lstIFR.Add(new CotacaoIFRDTO()
                {
                    Numero = 1,
                    Dd_Mm_Yyyy_Cotacao = _vm.ListaCotacoes[0].Dd_Mm_Yyyy,
                    ValorCotacao = naoNull(_vm.ListaCotacoes[0].ValorCotacao)
                });
                */

                for (int intDia = 1; intDia < _vm.ListaCotacoes.Count; intDia++)
                {
                    dcmDelta = naoNull(_vm.ListaCotacoes[intDia].ValorCotacao) -
                               naoNull(_vm.ListaCotacoes[intDia - 1].ValorCotacao);

                    if (dcmDelta > 0)
                    {
                        dcmGanho = dcmDelta;
                        dcmPerda = 0;
                    }
                    else
                    {
                        dcmGanho = 0;
                        dcmPerda = dcmDelta;
                    }

                    lstIFR.Add(new CotacaoIFRDTO()
                    {
                        Numero = intDia,
                        Dd_Mm_Yyyy_Cotacao = _vm.ListaCotacoes[intDia].Dd_Mm_Yyyy,
                        ValorCotacao = naoNull(_vm.ListaCotacoes[intDia].ValorCotacao),
                        Ganho = dcmGanho,
                        Perda = Math.Abs(dcmPerda)
                    });
                }

                for (int intDia = (_vm.MMDias - 1); intDia < lstIFR.Count; intDia++)
                {
                    if (intDia == 249)
                    {

                    }

                    lstIFR[intDia].RS = media(ref lstIFR, intDia);

                    if (lstIFR[intDia].RS != 0)
                        lstIFR[intDia].IFR = (100.0m - (100.0m / (1.0m + lstIFR[intDia].RS)));
                    else
                        lstIFR[intDia].IFR = 100;

                    if (lstIFR[intDia].IFR > 100)
                        lstIFR[intDia].IFR = 100;

                    if (lstIFR[intDia].IFR < 0)
                        lstIFR[intDia].IFR = 0;
                }

                _vm.ListaIFR = lstIFR;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
        }

        public async Task<bool> CarregarCotacoes()
        {
            bool boolOk = false;

            try
            {
                _vm.ListaCotacoes = await (new CotacaoDAO()).ListaCotacoesGrafico(_vm.AcaoSelecionada.Id,
                                                                                  _vm.PeriodoDe.ToString("yyyy/MM/dd"),
                                                                                  _vm.PeriodoAte.ToString("yyyy/MM/dd"));

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<bool> Iniciar()
        {
            bool boolOk = false;

            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg = new StringBuilder();

                _vm.PeriodoDe = DateTime.Today.AddYears(-1);
                _vm.PeriodoAte = DateTime.Today;

                List<int> lstDias = new List<int>();

                for (int intDia = 5; intDia < 31; intDia++)
                    lstDias.Add(intDia);

                _vm.ListaDiasMM = lstDias;

                _vm.MMDias = 14;

                _vm.ListaAcoes = await (new InvestimentoDAO()).ListaIdTicker();
                _vm.ListaCotacoes = new ObservableCollection<Model.DTO.CotacaoGraficoDTO>();
                _vm.ListaIFR = new List<CotacaoIFRDTO>();

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public bool PodePesquisar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if ((_vm.AcaoSelecionada == null) || (_vm.AcaoSelecionada.Id <= 0))
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione a Ação");
            }

            if ((_vm.PeriodoDe.Year < 2020))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Selecione a Data Inicial a Pesquisar");
            }

            if ((_vm.PeriodoAte < _vm.PeriodoDe))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 3 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- A Data Final deve ser maior do que a Data Inicial");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}