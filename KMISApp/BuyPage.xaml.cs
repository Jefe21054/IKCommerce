using KMISApp.Model;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class BuyPage : ContentPage
    {
        public BuyPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Cuenta> ventas = await App.MobileService.GetTable<Cuenta>().Where(p => p.UsuarioEmail != App.usuario.Email).ToListAsync();
            ventasListView.ItemsSource = ventas;
        }

        private void ventasListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new CreditCardPage());
        }
    }
}
