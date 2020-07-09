using KMISApp.Models;
using KMISApp.ViewModels.Commands;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KMISApp.ViewModels
{
    public class MyPublicationsPageViewModel : INotifyPropertyChanged
    {
        public SavePublicationsCommand SavePublicationsCommand { get; set; }

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

        private string nombreServicio;

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

        public MyPublicationsPageViewModel()
        {
            SavePublicationsCommand = new SavePublicationsCommand(this);
        }

        private void EmptyEntries()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        public async void Save()
        {
            try
            {
                int i = Index;
                string nombre;
                switch (i)
                {
                    case 0:
                        nombre = "Netflix";
                        break;
                    case 1:
                        nombre = "Spotify";
                        break;
                    case 2:
                        nombre = "Deezer";
                        break;
                    case 3:
                        nombre = "Disney+";
                        break;
                    case 4:
                        nombre = "Hulu";
                        break;
                    case 5:
                        nombre = "Nintendo Online";
                        break;
                    case 6:
                        nombre = "Amazon Prime Video";
                        break;
                    default:
                        nombre = null;
                        await App.Current.MainPage.DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                        EmptyEntries();
                        break;
                }

                Random rnd = new Random();
                int num = rnd.Next(1, 999999999);
                string number = num.ToString();
                cuenta.Id = number;
                cuenta.Email = Email;
                cuenta.Clave = Password;
                cuenta.Servicio = nombre;
                cuenta.UsuarioEmail = App.usuario.Email;

                bool isEmailEmpty = string.IsNullOrEmpty(Email);
                bool isPasswordEmpty = string.IsNullOrEmpty(Password);

                if (isEmailEmpty || isPasswordEmpty)
                {
                    await App.Current.MainPage.DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                }
                else
                {
                    Cuenta.Insert(cuenta);
                    await App.Current.MainPage.DisplayAlert("CORRECTO", "Registrado con Exito", "OK");
                    EmptyEntries();
                }
            }
            catch (MobileServiceInvalidOperationException)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                EmptyEntries();
            }
            catch (NullReferenceException)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                EmptyEntries();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                EmptyEntries();
            }
        }
    }
}
