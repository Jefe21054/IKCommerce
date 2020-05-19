using KMISApp.ViewModels;
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
        MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
    }
}
