using KMISApp.Model;
using KMISApp.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class SignUpPage : ContentPage
    {
        ApiServices apiServices = new ApiServices();

        public SignUpPage()
        {
            InitializeComponent();
        }

        private void EmptyEntrys()
        {
            emailEntry.Text = string.Empty;
            userNameEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
            confirmPasswordEntry.Text = string.Empty;
        }

        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                try
                {
                    var response = await apiServices.RegisterAsync(emailEntry.Text, userNameEntry.Text, passwordEntry.Text, confirmPasswordEntry.Text);
                    var username = await apiServices.UsernameAsync(emailEntry.Text, passwordEntry.Text);
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
                        EmptyEntrys();
                        await DisplayAlert("CORRECTO", "Registrado con Exito", "OK");
                        await Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Email or Username alredy exists!\nTry again, please.", "OK");
                        EmptyEntrys();
                    }
                }
                catch (MobileServiceInvalidOperationException)
                {
                    await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                    EmptyEntrys();
                }
                catch (NullReferenceException)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                    EmptyEntrys();
                }
                catch (Exception)
                {
                    await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                    EmptyEntrys();
                }
            }
            else
            {
                await DisplayAlert("ERROR", "Las claves no coinciden, ingresa de nuevo", "OK");
                EmptyEntrys();
            }
        }
    }
}
