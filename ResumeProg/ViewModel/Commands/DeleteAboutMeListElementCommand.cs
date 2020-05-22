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
    public class DeleteAboutMeListElementCommand : ICommand
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
            return true;
        }

        public void Execute(object parameter)
        {
            ListBox lb = parameter as ListBox;
            if (lb.SelectedItem != null)
                (lb.ItemsSource as ObservableCollection<AboutMe>).Remove(lb.SelectedItem as AboutMe);
        }
    }
}
