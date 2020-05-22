using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResumeProg.Model
{
    public class Info : INotifyPropertyChanged, IPropertyName
    {

        private string name;
        private DateTime date;
        private System.Drawing.Image photo;
        private string imagePath;
        private string phone;
        private string email;
        private double workExp;
        private ObservableCollection<WorkPlace> places = new ObservableCollection<WorkPlace>();
        private ObservableCollection<AboutMe> aboutMe = new ObservableCollection<AboutMe>();

        public string Name { get => name; set 
            {
                name = value;
                ExecutePropertyChange();
            } 
        }
        public DateTime Date { get => date; set
            {
                date = value;
                ExecutePropertyChange();
            }
        }
        [JsonIgnore]
        public System.Drawing.Image Photo { get => photo; set
            {
                photo = value;
                ExecutePropertyChange();
            }
        }

        public string ImagePath { get => imagePath; set
            {
                imagePath = value;
                if (imagePath != null)
                    Photo = System.Drawing.Image.FromFile(imagePath);
            }
        }
        public string Phone { get => phone; set
            {
                phone = value;
                ExecutePropertyChange();
            }
        }
        public string Email { get => email; set
            {
                email = value;
                ExecutePropertyChange();
            }
        }
        public double WorkExp { get => workExp; set
            {
                workExp = value;
                ExecutePropertyChange();
            }
        }
        public ObservableCollection<WorkPlace> Places { get => places; set
            {
                places = value;
                ExecutePropertyChange();
            }
        } 
        public ObservableCollection<AboutMe> AboutMe { get => aboutMe; set
            {
                aboutMe = value;
                ExecutePropertyChange();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecutePropertyChange([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Date":
                        return Date.ToShortDateString();
                    default:
                        return this.GetType().GetProperty(propertyName).GetValue(this).ToString();
                }
            }
        }

        public object this[string propertyName, bool? baka]
        {
            get
            {
                try
                {
                    return this.GetType().GetProperty(propertyName).GetValue(this);
                }
                catch
                {
                    return this.GetType().GetProperty(propertyName.Substring(1, propertyName.Length - 2)).GetValue(this);
                }
            }
        }

    }
}
