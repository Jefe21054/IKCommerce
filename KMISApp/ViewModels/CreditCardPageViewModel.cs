using System.ComponentModel;

namespace KMISApp.ViewModels
{
    public class CreditCardPageViewModel : INotifyPropertyChanged
    {
        private string cardNumber;

        public string CardNumber
        {
            get { return cardNumber; }
            set 
            { 
                cardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        private string cardCvv;

        public string CardCvv
        {
            get { return cardCvv; }
            set 
            { 
                cardCvv = value;
                OnPropertyChanged("CardCvv");
            }
        }

        private string cardExpirationDate;

        public string CardExpirationDate
        {
            get { return cardExpirationDate; }
            set 
            { 
                cardExpirationDate = value;
                OnPropertyChanged("CardExpirationDate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
