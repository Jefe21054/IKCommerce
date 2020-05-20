using KMISApp.ViewModels;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class SignUpPage : ContentPage
    {
        SignUpPageViewModel viewModel;

        public SignUpPage()
        {
            InitializeComponent();

            viewModel = new SignUpPageViewModel();
            BindingContext = viewModel;
        }
    }
}
