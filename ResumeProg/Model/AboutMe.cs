using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResumeProg.Model
{
    public class AboutMe : IPropertyName, INotifyPropertyChanged
    {

        private string value;
        public string Value { get => value; set 
            {
                this.value = value;
                OnChange();
            }
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    default:
                        return this.GetType().GetProperty(propertyName).GetValue(this).ToString();
                }
            }
        }

        public object this[string propertyName, bool? baka]
        {
            get
            {
                return this.GetType().GetProperty(propertyName).GetValue(this);
            }
        }

        public void OnChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
