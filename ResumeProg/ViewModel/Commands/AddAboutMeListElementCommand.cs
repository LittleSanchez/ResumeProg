using ResumeProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class AddAboutMeListElementCommand : ICommand
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
            var tb = parameter as AboutMe;
            string line = tb.Value;
            tb.Value = "";
            VM.Instance.Info.AboutMe.Add(new Model.AboutMe { Value = line });
        }
    }
}
