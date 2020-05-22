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
using System.Windows.Shapes;

namespace ResumeProg.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainInfoWindow.xaml
    /// </summary>
    public partial class MainInfoPage : Page, IPageInfo
    {
        public MainInfoPage()
        {
            InitializeComponent();
        }

        public string PageName { get; set; }

    }
}
