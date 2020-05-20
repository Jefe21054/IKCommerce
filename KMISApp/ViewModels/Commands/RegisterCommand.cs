using System;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class RegisterCommand : ICommand
    {
        public SignUpPageViewModel SignUpPageViewModel { get; set; }

        public RegisterCommand(SignUpPageViewModel signUpPageViewModel)
        {
            SignUpPageViewModel = signUpPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SignUpPageViewModel.Register();
        }
    }
}
