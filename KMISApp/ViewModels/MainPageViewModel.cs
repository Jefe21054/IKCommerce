using KMISApp.ViewModels.Commands;

namespace KMISApp.ViewModels
{
    public class MainPageViewModel
    {
        public NavigationOfferCommand NavigationOfferCommand { get; set; }

        public NavigationOwnerCommand NavigationOwnerCommand { get; set; }

        public MainPageViewModel()
        {
            NavigationOfferCommand = new NavigationOfferCommand(this);
            NavigationOwnerCommand = new NavigationOwnerCommand(this);
        }

        public void NavigateOffers()
        {
            App.Current.MainPage.Navigation.PushAsync(new MyPublicationsPage());
        }

        public void NavigateOwneds()
        {
            App.Current.MainPage.Navigation.PushAsync(new MyDBPage());
        }
    }
}
