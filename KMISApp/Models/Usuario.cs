using System.ComponentModel;

namespace KMISApp.Models
{
    public class Usuario : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Usuario usuario)
        {
            await App.MobileService.GetTable<Usuario>().InsertAsync(usuario);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
