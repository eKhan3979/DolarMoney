using System.Xml;

namespace DolarMoney.Model
{
    public class CotacaoInvestingModel: BaseModel
    {
        #region Variáveis da Classe

        private int _numero,
                    _idInvestimento;
        private string _ticker,
                       _cotacaoStr,
                       _volumeFormat,
                       _volumeStr;
        private decimal _cotacao,
                        _volume;        

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
        public string VolumeStr
        {
            get => _volumeStr;
            set
            {
                if (_volumeStr != value)
                {
                    _volumeStr = value;
                    OnPropertyChanged(nameof(VolumeStr));

                    try 
                    {
                        if (_volumeStr.Contains("M"))
                            Volume = 1000000 * decimal.Parse(_volumeStr.Replace("M", "")
                                                                       .Replace(",", "")
                                                                       .Replace(".", ","));
                        else
                            Volume = decimal.Parse(_volumeStr.Replace(",", "").Replace(".", ","));
                    }
                    catch { }
                }
            }
        }
        public decimal Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));

                    VolumeFormat = _volume.ToString("#,##0");
                }
            }
        }
        public string VolumeFormat
        {
            get => _volumeFormat;
            set
            {
                if (_volumeFormat != value)
                {
                    _volumeFormat = value;
                    OnPropertyChanged(nameof(VolumeFormat));
                }
            }
        }

        #endregion
    }
}