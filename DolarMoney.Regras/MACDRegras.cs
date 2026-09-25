using System;
using System.Collections.ObjectModel;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class MACDRegras: BaseRegras
    {
        #region Variáveis da Classe

        private MACDViewModel _vm;

        #endregion

        #region Construtor

        public MACDRegras(ref MACDViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private decimal naoNull(decimal? dcmValor)
        {
            return ((dcmValor != null) ? dcmValor.Value : 0);
        }

        private decimal? mediaDia(MACDViewModel vm, int intDia, int intDiasMM)
        {
            decimal? dcmTotal = 0;

            for (int intD = intDia - intDiasMM; intD < intDia; intD++)
                dcmTotal += ((vm.ListaCotacoes[intD].ValorCotacao != null) ? vm.ListaCotacoes[intD].ValorCotacao.Value : 0);

            return ((dcmTotal != null) ? dcmTotal.Value / (decimal)intDiasMM : null);
        }

        #endregion

        #region Público

        public void CalcularMediasAritmeticas(bool boolMediaCurta = true)
        {
            CotacaoGraficoDTO[] arrMedias = new CotacaoGraficoDTO[_vm.ListaCotacoes.Count];

            int intDiasMedia = ((boolMediaCurta) ? _vm.MMDias1 : _vm.MMDias2);

            decimal dcmTotal = 0;

            for (int intDia = intDiasMedia - 1; intDia < _vm.ListaCotacoes.Count - 1; intDia++)
            {
                dcmTotal = 0;

                for (int intParcial = intDia - intDiasMedia + 1; intParcial <= intDia; intParcial++)
                    dcmTotal +=  naoNull(_vm.ListaCotacoes[intParcial].ValorCotacao);

                arrMedias[intDia] = new CotacaoGraficoDTO()
                {
                    Numero = intDia,
                    DataCotacao = _vm.ListaCotacoes[intDia].DataCotacao,
                    ValorCotacao = (dcmTotal / intDiasMedia)
                };
            }

            if (boolMediaCurta)
                _vm.ListaMedia1 = new List<CotacaoGraficoDTO>(arrMedias);
            else
                _vm.ListaMedia2 = new List<CotacaoGraficoDTO>(arrMedias);
        }

        public void CalcularMACD()
        {
            try
            {
                // Média Custa

                decimal alfa = (2.0m / (_vm.MMDias1 + 1.0m));

                CotacaoGraficoDTO[] arrEMA = new CotacaoGraficoDTO[_vm.ListaMedia1.Count];

                for (int intDia = 0; intDia < _vm.MMDias1 - 1; intDia++)
                    arrEMA[intDia] = new CotacaoGraficoDTO()
                    {
                        Numero = intDia + 1
                    };

                arrEMA[_vm.MMDias1 - 1] = new CotacaoGraficoDTO()
                {
                    Numero = _vm.MMDias1,
                    ValorCotacao = _vm.ListaMedia1[_vm.MMDias1 - 1].ValorCotacao
                };

                for (int intDia = _vm.MMDias1; intDia < _vm.ListaMedia1.Count; intDia++)
                    arrEMA[intDia] = new CotacaoGraficoDTO()
                    {
                        Numero = _vm.MMDias1,
                        ValorCotacao = (_vm.ListaCotacoes[intDia].ValorCotacao - arrEMA[intDia - 1].ValorCotacao) * alfa +
                                        arrEMA[intDia - 1].ValorCotacao
                    };

                _vm.ListaEMA1 = new List<CotacaoGraficoDTO>(arrEMA);

                // Média Longa

                alfa = (2.0m / (_vm.MMDias2 + 1.0m));

                arrEMA = new CotacaoGraficoDTO[_vm.ListaMedia2.Count];

                for (int intDia = 0; intDia < _vm.MMDias2 - 1; intDia++)
                    arrEMA[intDia] = new CotacaoGraficoDTO()
                    {
                        Numero = intDia + 1
                    };

                arrEMA[_vm.MMDias2 - 1] = new CotacaoGraficoDTO()
                {
                    Numero = _vm.MMDias2,
                    ValorCotacao = _vm.ListaMedia2[_vm.MMDias2 - 1].ValorCotacao
                };

                for (int intDia = _vm.MMDias2; intDia < _vm.ListaMedia2.Count; intDia++)
                    arrEMA[intDia] = new CotacaoGraficoDTO()
                    {
                        Numero = _vm.MMDias2,
                        ValorCotacao = (_vm.ListaCotacoes[intDia].ValorCotacao - arrEMA[intDia - 1].ValorCotacao) * alfa +
                                        arrEMA[intDia - 1].ValorCotacao
                    };

                _vm.ListaEMA2 = new List<CotacaoGraficoDTO>(arrEMA);

                List<CotacaoMacdDTO> lstMACD = new List<CotacaoMacdDTO>();

                for (int intDia = 0; intDia < _vm.ListaCotacoes.Count; intDia++)
                    lstMACD.Add(new CotacaoMacdDTO()
                    {
                        Numero = _vm.ListaCotacoes[intDia].Numero,
                        DataCotacao = _vm.ListaCotacoes[intDia].DataCotacao,
                        ValorCotacao = (double)naoNull(_vm.ListaCotacoes[intDia].ValorCotacao),
                        ValorEMA1 = (double)naoNull(_vm.ListaEMA1[intDia].ValorCotacao),
                        ValorEMA2 = (double)naoNull(_vm.ListaEMA2[intDia].ValorCotacao),
                        ValorMACD = ((_vm.ListaEMA1[intDia].ValorCotacao != null) &&
                                     (_vm.ListaEMA2[intDia].ValorCotacao != null) 
                                     ? (double)(naoNull(_vm.ListaEMA1[intDia].ValorCotacao) - naoNull(_vm.ListaEMA2[intDia].ValorCotacao))
                                     : 0),
                        ValorMedia1 = ((_vm.ListaMedia1[intDia] != null) ? (double)naoNull(_vm.ListaMedia1[intDia].ValorCotacao) : 0),
                        ValorMedia2 = ((_vm.ListaMedia2[intDia] != null) ? (double)naoNull(_vm.ListaMedia2[intDia].ValorCotacao) : 0)
                    });

                double valorEMAMACD_1 = 0;

                for (int intDia = _vm.MMDias2 - 1; intDia < (_vm.MMDias2 + 9 - 1); intDia++)
                    valorEMAMACD_1 += lstMACD[intDia].ValorMACD;

                double fatorK = 2.0 / (9.0 + 1.0);

                lstMACD[_vm.MMDias2 + 9 - 1].ValorEMAMACD = valorEMAMACD_1 / 9.0;

                for (int intDia = _vm.MMDias2 + 9; intDia < lstMACD.Count; intDia++)
                    lstMACD[intDia].ValorEMAMACD = lstMACD[intDia].ValorMACD * fatorK + 
                                                   lstMACD[intDia - 1].ValorEMAMACD * (1.0 - fatorK);

                _vm.ListaMACD = lstMACD;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
        }

        public void CalcularMediaMovel(bool boolMM1 = true)
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                int intDiasMM = ((boolMM1 ? _vm.MMDias1 : _vm.MMDias2));

                List<CotacaoGraficoDTO> lstMM = new List<CotacaoGraficoDTO>();

                for (int intDia = 0; intDia < intDiasMM; intDia++)
                    lstMM.Add(new CotacaoGraficoDTO()
                    {
                        DataCotacao = _vm.ListaCotacoes[intDia].DataCotacao,
                        Numero = _vm.ListaCotacoes[intDia].Numero,
                        ValorCotacao = null
                    });

                for (int intDia = intDiasMM; intDia < _vm.ListaCotacoes.Count; intDia++)
                    lstMM.Add(new CotacaoGraficoDTO()
                    {
                        DataCotacao = _vm.ListaCotacoes[intDia].DataCotacao,
                        Numero = _vm.ListaCotacoes[intDia].Numero,
                        ValorCotacao = mediaDia(_vm, intDia, intDiasMM)
                    });

                if (boolMM1)
                    _vm.ListaMedia1 = lstMM;
                else
                    _vm.ListaMedia2 = lstMM;
            }
            catch (Exception excErro)
            {
                _vm.ErroMsg.AppendLine(excErro.Message);
                _vm.ErroIndex = 1;
            }
        }

        public async Task<bool> CarregarCotacoes()
        {
            bool boolOk = false;

            try
            {
                _vm.AnaliseAtual = await (new AnaliseDAO()).GetPorInvestimento(_vm.AcaoSelecionada.Id);

                if (_vm.AnaliseAtual.DataAnalise.Year == 1)
                    _vm.AnaliseAtual = new AnaliseModel()
                    {
                        DataAnalise = DateTime.Today,
                        DiasMM1 = _vm.MMDias1,
                        DiasMM2 = _vm.MMDias2,
                        IdInvestimento = _vm.AcaoSelecionada.Id,
                        IdTendencia = 0
                    };

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

        public async Task<bool> CarregarTendenciasAtuais()
        {
            bool boolOk = false;

            try
            {
                _vm.ListaTendenciasAtuais = await (new AnaliseDAO()).ListaTendenciasAtual();
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public async Task<bool> GravarAnalise()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            try
            {
                AnaliseModel analise = new AnaliseModel()
                {
                    DataAnalise = _vm.AnaliseAtual.DataAnalise,
                    DataCompra = _vm.AnaliseAtual.DataCompra,
                    DataVenda = _vm.AnaliseAtual.DataVenda,
                    DiasMM1 = _vm.AnaliseAtual.DiasMM1,
                    DiasMM2 = _vm.AnaliseAtual.DiasMM2,
                    IdInvestimento = _vm.AcaoSelecionada.Id,
                    IdTendencia = _vm.AnaliseAtual.IdTendencia
                };

                await (new AnaliseDAO()).Gravar(analise);
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

                _vm.PeriodoDe = DateTime.Today.AddYears(-1);
                _vm.PeriodoAte = DateTime.Today;

                _vm.ListaDiasMM1 = new List<int>()
                {
                    4, 5, 6, 7, 8, 9, 10, 11, 12, 14
                };

                _vm.ListaDiasMM2 = new List<int>()
                {
                    15, 18, 19, 20, 21, 22, 24, 26, 28, 30
                };

                _vm.MMDias1 = 12;
                _vm.MMDias2 = 26;

                _vm.ListaAcoes = await (new InvestimentoDAO()).ListaIdTicker();
                _vm.ListaCotacoes = new ObservableCollection<Model.DTO.CotacaoGraficoDTO>();

                List<IdDescricaoDTO> lstExpirado = new List<IdDescricaoDTO>()
                {
                    new IdDescricaoDTO() { Id = 0, Descricao = "Não" },
                    new IdDescricaoDTO() { Id = 1, Descricao = "Sim" }
                };

                _vm.ListaExpirado = lstExpirado;

                List<TendenciaModel> lstTendencia = new List<TendenciaModel>()
                {
                    new TendenciaModel() { IdTendencia = EnumTendencia.Indefinido.GetHashCode(), Tendencia = "Indefinido" },
                    new TendenciaModel() { IdTendencia = EnumTendencia.Alta.GetHashCode(), Tendencia = "Alta" },
                    new TendenciaModel() { IdTendencia = EnumTendencia.Baixa.GetHashCode(), Tendencia = "Baixa" }
                };

                _vm.ListaTendencia = lstTendencia;

                _vm.AnaliseAtual = new AnaliseModel()
                {
                    DataAnalise = DateTime.Today,
                    Expirado = 0,
                    IdTendencia = _vm.ListaTendencia[0].IdTendencia,
                    DiasMM1 = _vm.ListaDiasMM1[5],
                    DiasMM2 = _vm.ListaDiasMM2[5]
                };

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return boolOk;
        }

        public bool PodeGravarAnalise()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if ((_vm.AcaoSelecionada == null) ||
                (_vm.AcaoSelecionada.Id == 0))
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione a Ação a pesquisar");
            }

            if (_vm.AnaliseAtual.DataAnalise.CompareTo(new DateTime(2025, 12, 26)) < 0)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- A Data da Análise só pde ser posterior à 26/12/2025");
            }

            return (_vm.ErroIndex == 0);
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
