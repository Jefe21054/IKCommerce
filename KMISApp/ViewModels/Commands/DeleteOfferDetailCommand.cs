using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KMISApp.ViewModels.Commands
{
    public class DeleteOfferDetailCommand : ICommand
    {
        public OfferDetailPageViewModel OfferDetailPageViewModel { get; set; }

        public DeleteOfferDetailCommand(OfferDetailPageViewModel offerDetailPageViewModel)
        {
            OfferDetailPageViewModel = offerDetailPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
