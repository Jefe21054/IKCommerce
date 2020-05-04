using KMISApp.Model;
using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using KMISApp.Views;
using KMISApp.Services;
using System.Threading.Tasks;

namespace KMISApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("KMISApp.Assets.Images.web_hi_res_512.png", assembly);
        }

        public async void loginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            
            bool isEmailEmpty = string.IsNullOrEmpty(email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos!", "OK");
            }
            else
            {
                try
                {
                    var response = await apiServices.LoginAsync(email.Text, password.Text);
                    //var user = (await App.MobileService.GetTable<AspNetUsers>().Where(u => u.UserName == email.Text).ToListAsync()).FirstOrDefault();

                    if (response != null)
                    {
                        //App.usuario = user;
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Nombre de usuario o clave incorrectos.", "OK");
                    }
                }
                catch (NullReferenceException)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                    email.Text = string.Empty;
                    password.Text = string.Empty;
                }
                catch (Exception)
                {
                    await DisplayAlert("ERROR", "Hubo un error cuando intentaste ingresar.", "OK");
                }
                //email.Text = string.Empty;
                password.Text = string.Empty;
            }
        }

        void forgetPass_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ForgetPasswordPage());
            email.Text = string.Empty;
            password.Text = string.Empty;
        }

        void signUp_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
            email.Text = string.Empty;
            password.Text = string.Empty;
        }
    }
}
