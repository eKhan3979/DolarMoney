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
    public class InvestimentoRegras: BaseRegras
    {
        #region Variáveis da Classe

        private InvestimentoViewModel _vm;

        #endregion

        #region Construtor

        public InvestimentoRegras(ref InvestimentoViewModel vm) 
        {
            _vm = vm;
        }

        #endregion

        #region Público

        public void Edit(InvestimentoDTO investimento)
        {
            _vm.InvestimentoEdit = new InvestimentoModel()
            {
                IdInvestimento = investimento.IdInvestimento,
                IdTipoInvestimento = investimento.IdTipoInvestimento,
                Ativo = investimento.Ativo,
                DataCadastro = investimento.DataCadastro,
                Nome = investimento.Nome,
                Ticker = investimento.Ticker
            };
        }

        public bool Gravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            try
            {
                InvestimentoModel investimento = new InvestimentoModel()
                {
                    IdInvestimento = _vm.InvestimentoEdit.IdInvestimento,
                    IdTipoInvestimento = _vm.InvestimentoEdit.IdTipoInvestimento,
                    Ativo = _vm.InvestimentoEdit.Ativo,
                    DataCadastro = _vm.InvestimentoEdit.DataCadastro,
                    Nome = _vm.InvestimentoEdit.Nome,
                    Ticker = _vm.InvestimentoEdit.Ticker
                };

                var id = (new InvestimentoDAO()).Gravar(investimento);

                if (id != null)
                    try { _vm.InvestimentoEdit.IdInvestimento = (int)id; }
                    catch { }

                var edit = _vm.ListaInvestimentos.FirstOrDefault(t => t.IdInvestimento.Equals(_vm.InvestimentoEdit.IdInvestimento));

                if (edit != null)
                {
                    edit.Ticker = _vm.InvestimentoEdit.Ticker;
                    edit.Nome = _vm.InvestimentoEdit.Nome;
                    edit.Ativo = _vm.InvestimentoEdit.Ativo;
                    edit.DataCadastro = _vm.InvestimentoEdit.DataCadastro;
                }
                else
                {
                    _vm.ListaInvestimentos.Add(new InvestimentoDTO(investimento));

                    var lista = _vm.ListaInvestimentos.OrderBy(t => t.Ticker).ToList();

                    for (int intNumero = 0; intNumero < lista.Count; intNumero++)
                        lista[intNumero].Numero = intNumero + 1;

                    _vm.ListaInvestimentos = new ObservableCollection<InvestimentoDTO>(lista);
                }
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
                _vm.ListaAtivo = new ObservableCollection<CodigoDescricaoDTO>()
                {
                    new CodigoDescricaoDTO() { Check = true, Descricao = "Sim" },
                    new CodigoDescricaoDTO() { Check = false, Descricao = "Não" }
                };

                _vm.ListaTipoInvestimento = (new TipoInvestimentoDAO()).Lista(true);

                _vm.InvestimentoEdit = new InvestimentoModel()
                {
                    DataCadastro = DateTime.Today,
                    Ativo = true
                };

                if (_vm.ListaTipoInvestimento.Count > 0)
                    _vm.InvestimentoEdit.IdTipoInvestimento = _vm.ListaTipoInvestimento[0].IdTipoInvestimento;

                ObservableCollection<InvestimentoModel> lstInvest = await (new InvestimentoDAO()).Lista(_vm.InvestimentoEdit.IdTipoInvestimento);
                ObservableCollection<InvestimentoDTO> lstInvestimentos = new ObservableCollection<InvestimentoDTO>();

                foreach (InvestimentoModel investimento in lstInvest)
                {
                    lstInvestimentos.Add(new InvestimentoDTO(investimento));
                    lstInvestimentos[lstInvestimentos.Count - 1].Numero = lstInvestimentos.Count;
                }

                _vm.ListaInvestimentos = lstInvestimentos;
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
            _vm.InvestimentoEdit = new InvestimentoModel()
            {
                DataCadastro = DateTime.Today,
                Ativo = true
            };
        }

        public bool PodeGravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if (_vm.InvestimentoEdit.IdTipoInvestimento == 0)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione o Tipo de Investimento");
            }
            if (string.IsNullOrWhiteSpace(_vm.InvestimentoEdit.Ticker))
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Preencha o Ticker");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}