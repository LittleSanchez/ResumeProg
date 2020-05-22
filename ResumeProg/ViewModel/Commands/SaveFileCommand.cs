using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class SaveFileCommand : ICommand
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
            return !string.IsNullOrEmpty(VM.Instance.SaveLoadHelper.CustomFilePath);
        }

        public void Execute(object parameter)
        {
            VM.Instance.SaveLoadHelper.Save(VM.Instance.Info);
        }
    }
}
