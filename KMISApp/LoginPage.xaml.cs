using KMISApp.Model;
using KMISApp.Services;
using KMISApp.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new SocialLoginPageViewModel();

            Type assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("KMISApp.Assets.Images.web_hi_res_512.png", assembly);
        }

        private void VaciarCampos()
        {
            email.Text = string.Empty;
            password.Text = string.Empty;
        }

        public async void loginButton_Clicked(object sender, System.EventArgs e)
        {
            ApiServices apiServices = new ApiServices();

            bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos!", "OK");
            }
            else
            {
                try
                {
                    string response = await apiServices.LoginAsync(email.Text, password.Text);
                    string username = await apiServices.UsernameAsync(email.Text, password.Text);
                    Usuario user = (await App.MobileService.GetTable<Usuario>().Where(u => u.Email == username).ToListAsync()).FirstOrDefault();
                    if (response != null)
                    {
                        App.usuario = user;
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Nombre de usuario o clave incorrectos.", "OK");
                    }
                }
                catch (MobileServiceInvalidOperationException)
                {
                    await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                    VaciarCampos();
                }
                catch (NullReferenceException)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                    VaciarCampos();
                }
                catch (Exception)
                {
                    await DisplayAlert("ERROR", "Hubo un error cuando intentaste ingresar.", "OK");
                }
                //email.Text = string.Empty;
                password.Text = string.Empty;
            }
        }

        private void forgetPass_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ForgetPasswordPage());
            VaciarCampos();
        }

        private void signUp_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
            VaciarCampos();
        }
    }
}
