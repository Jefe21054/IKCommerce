using System.ComponentModel;

namespace KMISApp.ViewModels
{
    public class CreditCardPageViewModel : INotifyPropertyChanged
    {
        public string CardNumber { get; set; }
        public string CardCvv { get; set; }
        public string CardExpirationDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
