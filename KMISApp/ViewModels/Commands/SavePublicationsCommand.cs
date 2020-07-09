using System;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class SavePublicationsCommand : ICommand
    {
        public MyPublicationsPageViewModel MyPublicationsPageViewModel { get; set; }

        public SavePublicationsCommand(MyPublicationsPageViewModel myPublicationsPageViewModel)
        {
            MyPublicationsPageViewModel = myPublicationsPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MyPublicationsPageViewModel.Save();
        }
    }
}
