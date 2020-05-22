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
using AttachedCommandBehavior;
using ResumeProg.Model;

namespace ResumeProg.Views.Pages
{
    /// <summary>
    /// Interaction logic for WorkPlacesWindow.xaml
    /// </summary>
    public partial class WorkPlacesPage : Page, IPageInfo
    {
        public WorkPlacesPage()
        {
            InitializeComponent();
            listBox.Items.Clear();
            listBox.ItemsSource = VM.Instance.Info.Places;
        }

        public string PageName { get; set; }

    }
}
