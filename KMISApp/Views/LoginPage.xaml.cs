using KMISApp.Model;
using KMISApp.Services;
using KMISApp.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
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

        private void VaciarCampos()
        {
            email.Text = string.Empty;
            password.Text = string.Empty;
        }

        private void forgetPass_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ForgetPasswordPage());
            VaciarCampos();
        }

        private void signUp_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
            VaciarCampos();
        }
    }
}
