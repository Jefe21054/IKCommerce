using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMISApp
{
    public partial class App : Application
    {
        public static string DataBaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string dataBaseLocation)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            DataBaseLocation = dataBaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
