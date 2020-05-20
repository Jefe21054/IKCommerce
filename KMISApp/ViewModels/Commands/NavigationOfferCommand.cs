using System;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class NavigationOfferCommand : ICommand
    {
        public MainPageViewModel MainPageViewModel { get; set; }

        public NavigationOfferCommand(MainPageViewModel mainPageViewModel)
        {
            MainPageViewModel = mainPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainPageViewModel.NavigateOffers();
        }
    }
}
