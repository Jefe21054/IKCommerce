using System;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginPageViewModel LoginPageViewModel { get; set; }

        public LoginCommand(LoginPageViewModel loginPageViewModel)
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
            LoginPageViewModel.Login();
        }
    }
}
