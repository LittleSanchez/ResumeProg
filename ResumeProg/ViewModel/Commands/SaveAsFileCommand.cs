using Microsoft.Win32;
using ResumeProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class SaveAsFileCommand : ICommand
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
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "ResumeProg Save|*." + SaveLoad.SAVE_FILE_EXTENTION
            };
            if (sfd.ShowDialog() == true)
            {
                VM.Instance.SaveLoadHelper.Save(VM.Instance.Info, sfd.FileName);
            }
        }
    }
}
