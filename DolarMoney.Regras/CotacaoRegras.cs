using System;
using System.Collections.ObjectModel;
using System.Text;

using DolarMoney.Model;
using DolarMoney.Model.DTO;
using DolarMoney.DAO;
using DolarMoney.ViewModel;

namespace DolarMoney.Regras
{
    public class CotacaoRegras : BaseRegras
    {
        #region Variáveis da Classe

        private CotacaoViewModel _vm;

        #endregion

        #region Construtor

        public CotacaoRegras(ref CotacaoViewModel vm)
        {
            _vm = vm;
        }

        #endregion

        #region Público

        public async Task<bool> Iniciar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg = new StringBuilder();

            try
            {
                _vm.ListaInvestimentos = await (new InvestimentoDAO()).ListaIdTicker(true);

                _vm.Pesquisar = new CotacaoPesquisarDTO()
                {
                    PeriodoDe = new DateTime(2024, 1, 1),
                    PeriodoAte = DateTime.Today
                };

                _vm.Cadastro = new CotacaoModel();
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
            _vm.Cadastro = new CotacaoModel();
        }

        public async Task<bool> PesquisarPeriodo()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                _vm.ListaCotacoes = await (new CotacaoDAO()).ListaIdPeriodo(_vm.Pesquisar);
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);

                throw excErro;
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}