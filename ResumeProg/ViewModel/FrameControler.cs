using ResumeProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResumeProg.ViewModel
{
    public class FrameControler
    {
        public Frame MainFrame { get; private set; }

        public FrameControler(Frame frame)
        {
            MainFrame = frame;
            PageControler.InitializePages();
            frame.NavigationService.Navigate(PageControler.PageByIndex(0));
        }

        public bool CanNextPage()
        {
            Page currentPage = null;
            if (MainFrame != null)
                currentPage = MainFrame.Content as Page;
            Page newPage = PageControler.NextPage(currentPage);
            return newPage != currentPage;
        }

        public bool CanPreviewPage()
        {
            Page currentPage = null;
            if (MainFrame != null)
                currentPage = MainFrame.Content as Page;
            Page newPage = PageControler.PreviewPage(currentPage);
            return newPage != currentPage;
        }

        public bool NextPage()
        {
            Page currentPage = MainFrame.Content as Page;
            Page newPage = PageControler.NextPage(currentPage);
            if (newPage != currentPage)
            {
                MainFrame.Navigate(newPage);
                VM.Instance.MainWindow.CurrentPageName = (newPage as IPageInfo).PageName;
                return true;
            }
            return false;
        }

        public bool PreviewPage()
        {
            Page currentPage = MainFrame.Content as Page;
            Page newPage = PageControler.PreviewPage(currentPage);
            if (newPage != currentPage)
            {
                MainFrame.Navigate(newPage);
                VM.Instance.MainWindow.CurrentPageName = (newPage as IPageInfo).PageName;
                return true;
            }
            return false;
        }

        public void SetPageByIndex(int index)
        {
            try
            {
                var newPage = PageControler.PageByIndex(index);
                MainFrame.Navigate(newPage);
                VM.Instance.MainWindow.CurrentPageName = (newPage as IPageInfo).PageName;
            }
            catch (IndexOutOfRangeException ex)
            {

            }
        }
    }
}
