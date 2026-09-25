using System.Xml;

namespace DolarMoney.Model
{
    public class CotacaoInvestingModel: BaseModel
    {
        #region Variáveis da Classe

        private int _numero,
                    _idInvestimento;
        private string _ticker,
                       _cotacaoStr;
        private decimal _cotacao;        

        #endregion

        #region Construtor

        public CotacaoInvestingModel() { }

        #endregion

        #region Propriedades

        public int Numero
        {
            get => _numero;
            set
            {
                if (_numero != value)
                {
                    _numero = value;
                    OnPropertyChanged(nameof(Numero));
                }
            }
        }
        public int IdInvestimento
        {
            get => _idInvestimento;
            set
            {
                if (_idInvestimento != value)
                {
                    _idInvestimento = value;
                    OnPropertyChanged(nameof(IdInvestimento));
                }
            }
        }
        public string Ticker
        {
            get => _ticker;
            set
            {
                if (_ticker != value)
                {
                    _ticker = value;
                    OnPropertyChanged(nameof(Ticker));
                }
            }
        }
        public string CotacaoStr
        {
            get => _cotacaoStr;
            set
            {
                if (_cotacaoStr != value)
                {
                    _cotacaoStr = value;
                    OnPropertyChanged(nameof(CotacaoStr));

                    try { Cotacao = decimal.Parse(_cotacaoStr.Replace(",", "").Replace(".", ",")); }
                    catch { }
                }
            }
        }
        public decimal Cotacao
        {
            get => _cotacao;
            set
            {
                if (_cotacao != value)
                {
                    _cotacao = value;
                    OnPropertyChanged(nameof(Cotacao));
                }
            }
        }

        #endregion
    }
}