using System;
using System.ComponentModel;
using System.Text;

namespace DolarMoney.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Variáveis da Classe

        private int _erroIndex;
        private StringBuilder _erroMsg;
        private bool _iniciado;

        #endregion

        #region Construtor

        public BaseViewModel() { }

        #endregion

        #region Público

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public int ErroIndex
        {
            get { return _erroIndex; }
            set
            {
                _erroIndex = value;
                OnPropertyChanged("ErroIndex");
            }
        }
        public StringBuilder ErroMsg
        {
            get { return _erroMsg; }
            set
            {
                _erroMsg = value;
                OnPropertyChanged("ErroMsg");
            }
        }
        public bool Iniciado
        {
            get { return _iniciado; }
            set
            {
                _iniciado = value;
                OnPropertyChanged("Iniciado");
            }
        }

        #endregion
    }
}