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
    public class AddListElementCommand : ICommand
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
            //WorkPlace workPlace = (parameter as ListBox).Tag as WorkPlace;
            //return (
            //    !string.IsNullOrWhiteSpace(workPlace.Name) &&
            //    !string.IsNullOrWhiteSpace(workPlace.Post) &&
            //    DateTime.Parse(workPlace.StartDate) < DateTime.Parse(workPlace.EndDate)
            //    );
            return true;
        }

        public void Execute(object parameter)
        {
            ListBox lb = parameter as ListBox;
            WorkPlace wp = lb.Tag as WorkPlace;
            (lb.ItemsSource as ObservableCollection<WorkPlace>).Add(wp.Clone() as WorkPlace);
            wp.Clear();
        }
    }
}
