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

        private void EmptyEntries()
        {
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
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
                    EmptyEntries();
                }
            }
            catch (MobileServiceInvalidOperationException)
            {
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                EmptyEntries();
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                EmptyEntries();
            }
            catch (Exception)
            {
                await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                EmptyEntries();
            }
        }
    }
}
