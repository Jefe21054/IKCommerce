using KMISApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class MyPublicationsPage : ContentPage
    {
        Cuenta cuenta;
        
        public MyPublicationsPage()
        {
            InitializeComponent();

            cuenta = new Cuenta();
            containerStackLayout.BindingContext = cuenta;
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                int num = rnd.Next(1, 999999999);
                string number = num.ToString();
                cuenta.Id = number;
                cuenta.Servicio = servicePicker.SelectedItem.ToString();
                cuenta.UsuarioEmail = App.usuario.Email;

                bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
                bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

                if (isEmailEmpty || isPasswordEmpty)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                }
                else
                {
                    Cuenta.Insert(cuenta);
                    await DisplayAlert("CORRECTO", "Registrado con Exito", "OK");
                    emailEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                }
            }
            catch (MobileServiceInvalidOperationException msio)
            {
                Task<string> response = msio.Response.Content.ReadAsStringAsync();
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                emailEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (NullReferenceException)
            {
                _ = DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                emailEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (Exception)
            {
                _ = DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                emailEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
        }
    }
}
