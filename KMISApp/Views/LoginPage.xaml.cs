using KMISApp.ViewModels;
using System;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel viewModel;
        
        public LoginPage()
        {
            InitializeComponent();

            viewModel = new LoginPageViewModel();
            BindingContext = viewModel;

            Type assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("KMISApp.Assets.Images.web_hi_res_512.png", assembly);
        }
    }
}
