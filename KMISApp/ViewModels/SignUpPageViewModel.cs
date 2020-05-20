using KMISApp.Model;
using KMISApp.Services;
using KMISApp.ViewModels.Commands;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.ComponentModel;

namespace KMISApp.ViewModels
{
    public class SignUpPageViewModel : INotifyPropertyChanged
    {
        private ApiServices apiServices = new ApiServices();

        public RegisterCommand RegisterCommand { get; set; }

        public SignUpPageViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
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

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
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

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            { 
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EmptyEntries()
        {
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        public async void Register()
        {
            if (Password == ConfirmPassword)
            {
                try
                {
                    var response = await apiServices.RegisterAsync(Email, Username, Password, ConfirmPassword);
                    var username = await apiServices.UsernameAsync(Email, Password);
                    if (response)
                    {
                        Random rnd = new Random();
                        int num = rnd.Next(1, 999999999);
                        string number = num.ToString();

                        Usuario usuario = new Usuario()
                        {
                            Id = number,
                            Email = username
                        };
                        Usuario.Insert(usuario);
                        EmptyEntries();
                        await App.Current.MainPage.DisplayAlert("CORRECTO", "Registrado con Exito", "OK");
                        await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERROR", "Email or Username already exists!\nTry again, please.", "OK");
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
            else
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "Las claves no coinciden, ingresa de nuevo", "OK");
                EmptyEntries();
            }
        }
    }
}
