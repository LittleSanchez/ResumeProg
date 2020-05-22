using Spire.Doc.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeProg.Model.Export
{
    public class DocumentInfo
    {
        public string FilePath { get; set; }
        public string FileName { get => Path.GetFileNameWithoutExtension(FilePath); }
        public IDocument Document { get; set; }
        public System.Drawing.Image Image { get; set; }
    }

}
