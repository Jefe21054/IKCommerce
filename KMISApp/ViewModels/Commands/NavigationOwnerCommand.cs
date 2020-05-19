using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class NavigationOwnerCommand : ICommand
    {
        public MainPageViewModel MainPageViewModel { get; set; }

        public NavigationOwnerCommand(MainPageViewModel mainPageViewModel)
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
            MainPageViewModel.NavigateOwneds();
        }
    }
}
