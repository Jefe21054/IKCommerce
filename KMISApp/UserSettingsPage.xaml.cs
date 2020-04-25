using KMISApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMISApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSettingsPage : ContentPage
    {
        Usuario selectedPost;
        
        public UserSettingsPage()
        {

        }
        
        public UserSettingsPage(Usuario selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            emailLabel.Text = selectedPost.Email;
            userNameEntry.Text = selectedPost.Username;
            passwordEntry.Text = selectedPost.Clave;
        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Clave = passwordEntry.Text;
            selectedPost.Username = userNameEntry.Text;

            try
            {
                bool isEmailEmpty = string.IsNullOrEmpty(userNameEntry.Text);
                bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

                if (isEmailEmpty || isPasswordEmpty)
                {
                    await DisplayAlert("ERROR", "Por favor llena todos los campos", "OK");
                }
                else
                {
                    await App.MobileService.GetTable<Usuario>().UpdateAsync(selectedPost);
                    await DisplayAlert("CORRECTO", "Actualizado con Exito", "OK");
                    passwordEntry.Text = string.Empty;
                }
            }
            catch (MobileServiceInvalidOperationException msio)
            {
                Task<string> response = msio.Response.Content.ReadAsStringAsync();
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("ERROR", "Por favor llena todos los campos.", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (Exception)
            {
                await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
        }

        private async void eraseButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("CAUTION","Are you sure you want to erase your user?","YES","NO");
                if (answer)
                {
                    await App.MobileService.GetTable<Usuario>().DeleteAsync(selectedPost);
                    await DisplayAlert("CORRECTO", "Borrado con Exito", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            catch (MobileServiceInvalidOperationException msio)
            {
                Task<string> response = msio.Response.Content.ReadAsStringAsync();
                await DisplayAlert("ERROR", "No se puede conectar con la Base de Datos", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("ERROR", "Para borrar deben estar llenos todos los campos.", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
            catch (Exception)
            {
                await DisplayAlert("ERROR", "Algo salio mal :( \nIntentalo de nuevo", "OK");
                userNameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }
        }
    }
}