using KMISApp.ViewModels;
using Xamarin.Forms;

namespace KMISApp
{
    public partial class MyPublicationsPage : ContentPage
    {
        MyPublicationsPageViewModel viewModel; 

        public MyPublicationsPage()
        {
            InitializeComponent();

            viewModel = new MyPublicationsPageViewModel();
            BindingContext = viewModel;
        }
    }
}
