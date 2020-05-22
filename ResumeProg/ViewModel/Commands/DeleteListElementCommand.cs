using ResumeProg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class DeleteListElementCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }


        public bool CanExecute(object parameter)
        {
            return (parameter is ListBox && (parameter as ListBox) != null && (parameter as ListBox).SelectedItem != null);
            
        }

        public void Execute(object parameter)
        {
            ListBox lb = parameter as ListBox;
            if (lb.SelectedItem != null)
                (lb.ItemsSource as ObservableCollection<WorkPlace>).Remove(lb.SelectedItem as WorkPlace);
        }
    }
}
