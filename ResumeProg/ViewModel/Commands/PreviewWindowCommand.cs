using ResumeProg.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class PreviewWindowCommand : ICommand
    {
#pragma warning disable CS0067 // The event 'PreviewWindowCommand.CanExecuteChanged' is never used
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
#pragma warning restore CS0067 // The event 'PreviewWindowCommand.CanExecuteChanged' is never used

        public bool CanExecute(object parameter)
        {
            return VM.Instance.Frame.CanPreviewPage();
        }

        public void Execute(object parameter)
        {
            VM.Instance.Frame.PreviewPage();
        }
    }
}
