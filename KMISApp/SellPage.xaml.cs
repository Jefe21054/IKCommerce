using KMISApp.Model;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class SellPage : ContentPage
    {
        public SellPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Cuenta> ofertas = await App.MobileService.GetTable<Cuenta>().Where(p => p.UsuarioEmail == App.usuario.Email).ToListAsync();
            cuentaListView.ItemsSource = ofertas;
        }

        private void cuentaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
