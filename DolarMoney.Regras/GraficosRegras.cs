using System;
using System.Collections.ObjectModel;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class GraficosRegras: BaseRegras
    {
        #region Variáveis da Classe

        private GraficosViewModel _vm;

        #endregion

        #region Construtor

        public GraficosRegras(ref GraficosViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Private

        private decimal? mediaDia(GraficosViewModel vm, int intDia, int intDiasMM)
        {
            decimal? dcmTotal = 0;

            for (int intD = intDia - intDiasMM; intD < intDia; intD++)
                dcmTotal += ((vm.ListaCotacoes[intD].ValorCotacao != null) ? vm.ListaCotacoes[intD].ValorCotacao.Value : 0);

            return ((dcmTotal != null) ? dcmTotal.Value / (decimal)intDiasMM : null);
        }

        #endregion

        #region Público

        public void CalcularMediaMovel(bool boolMM1 = true)
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                int intDiasMM = ((boolMM1 ? _vm.MMDias1 : _vm.MMDias2));

                ObservableCollection<CotacaoGraficoDTO> lstMM = new ObservableCollection<CotacaoGraficoDTO>();

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
                    _vm.ListaMM1 = lstMM;
                else
                    _vm.ListaMM2 = lstMM;
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

                _vm.ListaDiasMM1 = new ObservableCollection<int>()
                {
                    4, 5, 6, 7, 8, 9, 10, 11, 12, 14
                };

                _vm.ListaDiasMM2 = new ObservableCollection<int>()
                {
                    15, 18, 19, 20, 21, 22, 24, 26, 28, 30
                };

                _vm.MMDias1 = _vm.ListaDiasMM1[5];
                _vm.MMDias2 = _vm.ListaDiasMM2[5];

                _vm.ListaAcoes = await (new InvestimentoDAO()).ListaIdTicker();
                _vm.ListaCotacoes = new ObservableCollection<Model.DTO.CotacaoGraficoDTO>();

                ObservableCollection<IdDescricaoDTO> lstExpirado = new ObservableCollection<IdDescricaoDTO>()
                {
                    new IdDescricaoDTO() { Id = 0, Descricao = "Não" },
                    new IdDescricaoDTO() { Id = 1, Descricao = "Sim" }
                };

                _vm.ListaExpirado = lstExpirado;

                ObservableCollection<TendenciaModel> lstTendencia = new ObservableCollection<TendenciaModel>()
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