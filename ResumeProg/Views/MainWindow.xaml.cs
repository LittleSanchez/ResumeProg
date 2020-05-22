using Microsoft.Win32;
using ResumeProg.Model;
using ResumeProg.ViewModel;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ResumeProg.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            Frame frame = new Frame
            {
                NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden
            };
            VM.Instance.SetupData(this);
            VM.Instance.SetupPages(frame);
            DataContext = this;
            InitializeComponent();
            framePane.Child = frame;
            

            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == true)
            //{
            //    Document document = new Document(ofd.FileName);
            //    KeywordsConverter converter = new KeywordsConverter(VM.Instance.Info, document);
            //    converter.ConvertDocumentAsync();
            //}
        }

        private string currentPageName;
        public string CurrentPageName { get => currentPageName; set
            {
                currentPageName = value;
                OnChange();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChange([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
