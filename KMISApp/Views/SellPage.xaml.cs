using KMISApp.Models;
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
            List<Cuenta> ofertas = await Cuenta.Read();
            cuentaListView.ItemsSource = ofertas;
        }

        private void cuentaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Cuenta selectedPost = cuentaListView.SelectedItem as Cuenta;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new OfferDetailPage(selectedPost));
            }
        }
    }
}
