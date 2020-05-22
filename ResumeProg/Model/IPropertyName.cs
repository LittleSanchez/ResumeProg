using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeProg.Model
{
    public interface IPropertyName
    {
        string this[string propertyName] { get; }
        object this[string propertyName, bool? baka] { get; }

    }
}
