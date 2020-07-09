using KMISApp.Models;
using KMISApp.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMISApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferDetailPage : ContentPage
    {
        Cuenta selectedPost;

        public OfferDetailPage()
        {

        }

        public OfferDetailPage(Cuenta selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            emailEntry.Text = selectedPost.Email;
            passwordEntry.Text = selectedPost.Clave;
            servicePicker.SelectedItem = selectedPost.Servicio;
        }

        private void EmptyEntrys()
        {
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Email = emailEntry.Text;
            selectedPost.Clave = passwordEntry.Text;
            selectedPost.Servicio = servicePicker.SelectedItem.ToString();

            try
            {
                bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
                bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

                if (isEmailEmpty || isPasswordEmpty)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                }
                else
                {
                    Cuenta.Update(selectedPost);
                    await DisplayAlert("CORRECTO", "Actualizado con Exito", "OK");
                    EmptyEntrys();
                }
            }
            catch (MobileServiceInvalidOperationException msio)
            {
                Task<string> response = msio.Response.Content.ReadAsStringAsync();
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                EmptyEntrys();
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos.", "OK");
                EmptyEntrys();
            }
            catch (Exception)
            {
                await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                EmptyEntrys();
            }
        }

        private async void eraseButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("CAUTION", "Are you sure you want to erase your offer?", "YES", "NO");
                if (answer)
                {
                    Cuenta.Delete(selectedPost);
                    await DisplayAlert("CORRECTO", "Borrado con Exito", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
            }
            catch (MobileServiceInvalidOperationException msio)
            {
                Task<string> response = msio.Response.Content.ReadAsStringAsync();
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                EmptyEntrys();
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("ERROR", "Para borrar deben estar llenos todos los campos.", "OK");
                EmptyEntrys();
            }
            catch (Exception)
            {
                await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                EmptyEntrys();
            }
        }
    }
}