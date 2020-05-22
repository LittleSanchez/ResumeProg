using ResumeProg.Model;
using ResumeProg.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeProg.Views.Pages
{
    /// <summary>
    /// Interaction logic for AboutMePage.xaml
    /// </summary>
    public partial class AboutMePage : Page, IPageInfo
    {
        public AboutMePage()
        {
            InitializeComponent();
            listBox.Items.Clear();
            listBox.ItemsSource = VM.Instance.Info.AboutMe;
        }

        public string PageName { get; set; }
    }
}
