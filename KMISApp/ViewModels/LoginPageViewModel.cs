using KMISApp.Model;
using KMISApp.Services;
using KMISApp.ViewModels.Commands;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KMISApp.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private ApiServices apiServices = new ApiServices();

        public LoginCommand LoginCommand { get; set; }

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

        public LoginPageViewModel()
        {
            LoginCommand = new LoginCommand(this);
            OnLoginCommand = new Command<AuthNetwork>(async (data) => await LoginAsync(data));
        }

        private void VaciarCampos()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        public async void Login()
        {
            bool isEmailEmpty = string.IsNullOrEmpty(Email);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", "Por favor llena todos los campos!", "OK");
            }
            else
            {
                try
                {
                    string response = await apiServices.LoginAsync(Email, Password);
                    string username = await apiServices.UsernameAsync(Email, Password);
                    Usuario user = (await App.MobileService.GetTable<Usuario>().Where(u => u.Email == username).ToListAsync()).FirstOrDefault();
                    if (response != null)
                    {
                        App.usuario = user;
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERROR", "Nombre de usuario o clave incorrectos.", "OK");
                    }
                }
                catch (MobileServiceInvalidOperationException)
                {
                    await App.Current.MainPage.DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                    VaciarCampos();
                }
                catch (NullReferenceException)
                {
                    await App.Current.MainPage.DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                    VaciarCampos();
                }
                catch (Exception)
                {
                    await App.Current.MainPage.DisplayAlert("ERROR", "Hubo un error cuando intentaste ingresar.", "OK");
                }
                VaciarCampos();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand OnLoginCommand { get; set; }

        IFacebookClient _facebookService = CrossFacebookClient.Current;
        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        public ObservableCollection<AuthNetwork> AuthenticationNetworks { get; set; } = new ObservableCollection<AuthNetwork>()
        {
            new AuthNetwork()
            {
                Name = "Facebook",
                Icon = "ic_fb",
                Foreground = "#FFFFFF",
                Background = "#4768AD"
            },
              new AuthNetwork()
            {
                Name = "Google",
                Icon = "ic_google",
                Foreground = "#000000",
                Background ="#E6E6E6"
            }
        };

        private async Task LoginAsync(AuthNetwork authNetwork)
        {
            switch (authNetwork.Name)
            {
                case "Facebook":
                    await LoginFacebookAsync(authNetwork);
                    break;
                case "Google":
                    await LoginGoogleAsync(authNetwork);
                    break;
            }
        }

        private async Task LoginFacebookAsync(AuthNetwork authNetwork)
        {
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null)
                    {
                        return;
                    }

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            FacebookProfile facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            Random rnd = new Random();
                            int num = rnd.Next(1, 999999999);
                            string number = num.ToString();
                            Usuario socialLoginData = new Usuario
                            {
                                Id = number,
                                Email = facebookProfile.Email
                            };
                            Usuario.Insert(socialLoginData);
                            App.usuario = socialLoginData;
                            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task LoginGoogleAsync(AuthNetwork authNetwork)
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.AccessToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            string googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            Random rnd = new Random();
                            int num = rnd.Next(1, 999999999);
                            string number = num.ToString();
                            Usuario socialLoginData = new Usuario
                            {
                                Id = number,
                                Email = _googleService.CurrentUser.Email
                            };
                            Usuario.Insert(socialLoginData);
                            App.usuario = socialLoginData;
                            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
