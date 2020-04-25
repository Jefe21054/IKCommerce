using KMISApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            if(idEntry.Text.Length == 10)
            {
                if (passwordEntry.Text == confirmPasswordEntry.Text)
                {
                    //We can register
                    Usuario user = new Usuario()
                    {
                        Email = emailEntry.Text,
                        Clave = passwordEntry.Text,
                        Username = userNameEntry.Text,
                        Id = idEntry.Text
                    };

                    try
                    {
                        await App.MobileService.GetTable<Usuario>().InsertAsync(user);
                        emailEntry.Text = string.Empty;
                        userNameEntry.Text = string.Empty;
                        idEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;
                        confirmPasswordEntry.Text = string.Empty;
                        await DisplayAlert("CORRECTO", "Registrado con Exito", "OK");
                        await Navigation.PushAsync(new LoginPage());
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        Task<string> response = ex.Response.Content.ReadAsStringAsync();
                        await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                        emailEntry.Text = string.Empty;
                        userNameEntry.Text = string.Empty;
                        idEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;
                        confirmPasswordEntry.Text = string.Empty;
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                        emailEntry.Text = string.Empty;
                        userNameEntry.Text = string.Empty;
                        idEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;
                        confirmPasswordEntry.Text = string.Empty;
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "Las claves no coinciden, ingresa de nuevo", "OK");
                    emailEntry.Text = string.Empty;
                    userNameEntry.Text = string.Empty;
                    idEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                    confirmPasswordEntry.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("ERROR", "La cedula obligatoriamente tiene 10 digitos", "OK");
                idEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
                confirmPasswordEntry.Text = string.Empty;
            }
        }
    }
}
