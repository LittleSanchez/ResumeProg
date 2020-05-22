using ResumeProg.Model.Export;
using ResumeProg.ViewModel;
using Spire.Doc.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ResumeProg.Model
{
    public class ExportControler
    {
        public const string TEMPLATES_FOLDER = "Templates";
        public static ExportControler Instance { get; set; } = new ExportControler();

        public ObservableCollection<DocumentInfo> Documents { get; set; } = new ObservableCollection<DocumentInfo>();

        public void Setup()
        {
            var dispatcher = VM.Instance.MainWindow.Dispatcher;
            dispatcher.Invoke(() => Documents.Clear());
            var files = OpenDocumentHelper.GetAllTemplates(TEMPLATES_FOLDER);
            List<DocumentInfo> col = OpenDocumentHelper.GetDocuments(files);
            foreach (var it in col)
            {
                it.Image = it.Document.SaveToImages(0, Spire.Doc.Documents.ImageType.Bitmap);
                dispatcher.Invoke(() => Documents.Add(it));
                  
            }
        }

        private ExportControler() { }
    }
}
