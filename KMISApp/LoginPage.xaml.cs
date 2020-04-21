using KMISApp.Model;
using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

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
                    var user = (await App.MobileService.GetTable<Usuario>().Where(u => u.Email == email.Text).ToListAsync()).FirstOrDefault();

                    if (user != null)
                    {
                        App.usuario = user;
                        if (user.Clave == password.Text)
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await DisplayAlert("ERROR", "Correo electronico o clave incorrectos.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Hubo un error cuando intentaste ingresar.", "OK");
                    }
                }
                catch(MobileServiceInvalidOperationException ex)
                {
                    await DisplayAlert("ERROR", "Hubo un error cuando intentaste ingresar.", "OK");
                }
                catch (Exception exe)
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
