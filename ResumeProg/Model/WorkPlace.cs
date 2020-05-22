using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResumeProg.Model
{
    public class WorkPlace : ICloneable, INotifyPropertyChanged, IPropertyName
    {
        private string name;
        private string post;
        private DateTime startDate;
        private DateTime endDate;

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecutePropertyChange([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public WorkPlace()
        {
            StartDateString = EndDateString = DateTime.Now.ToLongDateString();
        }

        public string Name
        {
            get => name; set
            {
                name = value;
                ExecutePropertyChange();
            }
        }
        public string StartDateString { get => startDate.ToShortDateString(); set { startDate = DateTime.Parse(value); ExecutePropertyChange(); } }
        public string EndDateString { get => endDate.ToShortDateString(); set { endDate = DateTime.Parse(value); ExecutePropertyChange(); } }
        public DateTime StartDate { get => startDate; set { startDate = value; ExecutePropertyChange(); } }
        public DateTime EndDate { get => endDate; set { endDate = value; ExecutePropertyChange(); } }
        public string Post { get => post; set { post = value; ExecutePropertyChange(); } }

        public object Clone()
        {
            return new WorkPlace
            {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate,
                Post = Post
            };
        }

        public void Clear()
        {
            Name = Post = "";
            StartDate = EndDate = DateTime.Now;
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "StartDate":
                    case "EndDate":
                        return ((DateTime)this.GetType().GetProperty(propertyName).GetValue(this)).ToShortDateString();
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
    }
}
