using System.ComponentModel;
using KMISApp.ViewModels;
using Xamarin.Forms;

namespace KMISApp.Views
{
    [DesignTimeVisible(false)]
    public partial class CreditCardPage : ContentPage
    {
        public CreditCardPage()
        {
            InitializeComponent();
            this.BindingContext = new CreditCardPageViewModel();
        }
    }
}