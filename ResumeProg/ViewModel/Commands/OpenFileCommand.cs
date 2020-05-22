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
    public class OpenFileCommand : ICommand
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
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "ResumeProf Save|*." + SaveLoad.SAVE_FILE_EXTENTION
            };
            if (ofd.ShowDialog() == true)
            {
                VM.Instance.SaveLoadHelper.Save(VM.Instance.Info, VM.Instance.SaveLoadHelper.CustomFilePath);
                VM.Instance.Info = VM.Instance.SaveLoadHelper.Load(ofd.FileName);
                VM.Instance.SetupPages(VM.Instance.Frame.MainFrame);
            }
        }
    }
}
