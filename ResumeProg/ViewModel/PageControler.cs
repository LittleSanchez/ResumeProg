using ResumeProg.Model;
using ResumeProg.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResumeProg.ViewModel
{
    public static class PageControler
    {

        private static List<Page> pages = new List<Page>();
        public static int CurrentIndex = 0;

        public static void InitializePages()
        {
            pages.Clear();

            //Export page init
            var exportPage = new ExportPage() { PageName = "Експорт" };
            var exportControlerInstance = exportPage.DataContext as ExportControler;
            Task.Run(() => exportControlerInstance.Setup());
            pages.AddRange(new Page[]
            {
                //new StartPage(),
                new MainInfoPage(){ PageName = "Головна інформація" },
                new WorkPlacesPage(){ PageName = "Досвід роботи" },
                new AboutMePage(){ PageName = "Про себе" },
                exportPage,
            });

            foreach (Page page in pages)
            {
                page.ShowsNavigationUI = false;
            }
        }

        public static Page NextPage(Page currentPage)
        {
            int index = pages.IndexOf(currentPage);
            if (index != -1)
            {
                CurrentIndex = index;
                return pages[Math.Min(index + 1, pages.Count - 1)];
            }
            return pages[0];
        }

        public static Page PreviewPage(Page currentPage)
        {
            int index = pages.IndexOf(currentPage);
            if (index != -1)
            {
                CurrentIndex = index;
                return pages[Math.Max(index - 1, 0)];
            }
            return pages[0];
        }

        public static Page PageByIndex(int index)
        {
            if (index >= 0 && index < pages.Count) 
                return pages[index];
            throw new IndexOutOfRangeException();
        }
    }
}
