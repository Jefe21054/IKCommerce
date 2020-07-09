using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KMISApp.Models
{
    public class Cuenta : INotifyPropertyChanged
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

        private string clave;

        public string Clave
        {
            get { return clave; }
            set 
            { 
                clave = value;
                OnPropertyChanged("Clave");
            }
        }

        private string servicio;

        public string Servicio
        {
            get { return servicio; }
            set 
            { 
                servicio = value;
                OnPropertyChanged("Servicio");
            }
        }

        private string usuarioEmail;

        public string UsuarioEmail
        {
            get { return usuarioEmail; }
            set 
            { 
                usuarioEmail = value;
                OnPropertyChanged("UsuarioEmail");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Cuenta cuenta)
        {
            await App.MobileService.GetTable<Cuenta>().InsertAsync(cuenta);
        }

        public static async Task<List<Cuenta>> Read()
        {
            List<Cuenta> ofertas = await App.MobileService.GetTable<Cuenta>().Where(p => p.UsuarioEmail == App.usuario.Email).ToListAsync();
            return ofertas;
        }

        public static async void Update(Cuenta cuenta)
        {
            await App.MobileService.GetTable<Cuenta>().UpdateAsync(cuenta);
        }

        public static async void Delete(Cuenta cuenta)
        {
            await App.MobileService.GetTable<Cuenta>().DeleteAsync(cuenta);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
