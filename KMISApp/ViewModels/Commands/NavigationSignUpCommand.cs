using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class NavigationSignUpCommand : ICommand
    {
        public LoginPageViewModel LoginPageViewModel { get; set; }

        public NavigationSignUpCommand(LoginPageViewModel loginPageViewModel)
        {
            LoginPageViewModel = loginPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginPageViewModel.NavigateSignUp();
        }
    }
}
