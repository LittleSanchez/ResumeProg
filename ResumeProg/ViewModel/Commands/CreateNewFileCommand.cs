using ResumeProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class CreateNewFileCommand : ICommand
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
            var mboxRes = MessageBox.Show("You'll lose all unsaved data! Continue?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mboxRes == MessageBoxResult.Yes)
            {
                //SaveLoad.RemoveTempSave();
                VM.Instance.Info = VM.Instance.SaveLoadHelper.Load(SaveLoad.NEW_PROJECT_FILE);
                VM.Instance.SetupPages(VM.Instance.Frame.MainFrame);
                VM.Instance.SaveLoadHelper.CustomFilePath = null;
            }
        }
    }
}
