using ResumeProg.Model.Export;
using Spire.Doc.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResumeProg.Model
{
    public static class OpenDocumentHelper
    {

        public static IDocument OpenDocument(string path)
        {
            IDocument document = null;
            switch (Path.GetExtension(path))
            {
                case ".doc":
                case ".docx":
                    document = new Spire.Doc.Document(path);
                    break;
                case ".pdf":
                    //MessageBox.Show("Error!", "Programm does not support this type of files", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception("Invalid file type");
                    break;
            }
            return document;
        }

        public static string[] GetAllTemplates(string folder)
        {
            List<string> templates = new List<string>();
            templates.AddRange(Directory.GetFiles(folder));
            return templates.ToArray();
        }

        public static List<DocumentInfo> GetDocuments(string[] files)
        {
            List<DocumentInfo> documents = new List<DocumentInfo>();
            foreach (var file in files)
            {
                try
                {
                    IDocument doc = OpenDocument(file);
                    documents.Add(new DocumentInfo { Document = doc, FilePath = file });
                }
                catch { }
            }    
            return documents;
        }
    }
}
