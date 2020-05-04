using KMISApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class App : Application
    {
        public static string DataBaseLocation = string.Empty;
        public static MobileServiceClient MobileService = new MobileServiceClient("https://ikcommerce.azurewebsites.net");
        public static AspNetUsers usuario = new AspNetUsers();

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
