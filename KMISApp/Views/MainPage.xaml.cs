using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace KMISApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void offer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyPublicationsPage());
        }

        private void owned_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyDBPage());
        }
    }
}
