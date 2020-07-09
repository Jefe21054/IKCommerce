using KMISApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KMISApp.ViewModels
{
    public class OfferDetailPageViewModel : INotifyPropertyChanged
    {
        public Cuenta cuenta = new Cuenta();

        public IList<Servicios> Servicios { get { return ServiciosData.Servicios; } }

        Servicios selectedService;

        public Servicios SelectedService
        {
            get { return selectedService; }
            set
            {
                if (selectedService != value)
                {
                    selectedService = value;
                    OnPropertyChanged("SelectedService");
                }
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

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged("Index");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Update()
        {

        }

        public async void Delete()
        {

        }
    }
}
