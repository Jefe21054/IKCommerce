using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        void offer_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MyPublicationsPage());
        }

        void owned_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MyDBPage());
        }
    }
}
