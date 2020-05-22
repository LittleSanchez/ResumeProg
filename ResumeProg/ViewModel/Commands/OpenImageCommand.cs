using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ResumeProg.ViewModel.Commands
{
    public class OpenImageCommand : ICommand
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
            if (!string.IsNullOrEmpty(VM.Instance.Info.ImagePath) && parameter != null)
                (parameter as Image).Source = new BitmapImage(new Uri(VM.Instance.Info.ImagePath));
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JPG|*.jpg|PNG|*.png|Any files|*.*"
            };
            if (ofd.ShowDialog() == true)
            {
                VM.Instance.Info.ImagePath = ofd.FileName;
                (parameter as Image).Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
