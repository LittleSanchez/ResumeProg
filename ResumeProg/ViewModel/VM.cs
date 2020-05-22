using ResumeProg.Model;
using ResumeProg.ViewModel.Commands;
using ResumeProg.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ResumeProg.ViewModel
{
    public class VM 
    {

        #region Properties

        public static VM Instance { get; set; } = new VM();
        public MainWindow MainWindow { get; set; }
        public Info Info { get; set; } = new Info();
        public SaveLoad SaveLoadHelper { get; set; } = new SaveLoad();
        public string FilePath { get; set; }
        public FrameControler Frame { get; set; } = null;
        #endregion

        #region Setup

        public void SetupData(MainWindow window)
        {
            MainWindow = window;
            Info = SaveLoadHelper.Load();
            MainWindow.Title = SaveLoad.PROGRAM_FOLDER;            

            { //Window init
                MainWindow.Closing += SaveOnClose;
            }
        }

        public void SetupPages(Frame frame)
        {
            if (frame == null) return;
            Frame = new FrameControler(frame);
            Frame.SetPageByIndex(PageControler.CurrentIndex);
        }


        #endregion
        private void SaveOnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveLoadHelper.Save(Info, SaveLoad.SAVE_FILE_PATH);
        }

        public void Restart()
        {
            MainWindow.Dispatcher.Invoke(() => Restart());
        }

        #region Commands
        public OpenImageCommand OpenImageCommand { get; set; } = new OpenImageCommand();
        public AddListElementCommand AddListElementCommand { get; set; } = new AddListElementCommand();
        public DeleteListElementCommand DeleteListElementCommand { get; set; } = new DeleteListElementCommand();
        public NextWindowCommand NextWindowCommand { get; set; } = new NextWindowCommand();
        public PreviewWindowCommand PreviewWindowCommand { get; set; } = new PreviewWindowCommand();
        public OpenFileCommand OpenFileCommand { get; set; } = new OpenFileCommand();
        public SaveFileCommand SaveFileCommand { get; set; } = new SaveFileCommand();
        public SaveAsFileCommand SaveAsFileCommand { get; set; } = new SaveAsFileCommand();
        public AddAboutMeListElementCommand AddAboutMeListElementCommand { get; set; } = new AddAboutMeListElementCommand();
        public DeleteAboutMeListElementCommand DeleteAboutMeListElementCommand { get; set; } = new DeleteAboutMeListElementCommand();
        public ExportDocumentCommand ExportDocumentCommand { get; set; } = new ExportDocumentCommand();
        public CreateNewFileCommand CreateNewFileCommand { get; set; } = new CreateNewFileCommand();

        #endregion
    }
}
