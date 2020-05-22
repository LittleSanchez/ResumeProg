using Microsoft.Win32;
using ResumeProg.Model;
using ResumeProg.Model.Export;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeProg.ViewModel.Commands
{
    public class ExportDocumentCommand : ICommand
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
            var list = parameter as ListBox;
            return list?.SelectedItem != null;
        }

        public void Execute(object parameter)
        {
            SaveFileDialog ofd = new SaveFileDialog
            {
                Filter = "Word Doc|*.doc|Word Docx|*.docx",
            };
            if (ofd.ShowDialog() == true)
            {
                var list = parameter as ListBox;
                var doc = list.SelectedItem as DocumentInfo;
                var exporter = new KeywordsConverter(VM.Instance.Info, doc.Document);
                exporter.ConvertDocumentAsync(ofd.FileName);
            }
            
        }
    }
}
