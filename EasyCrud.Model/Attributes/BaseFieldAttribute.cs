using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseFieldAttribute : Attribute
    {
        public abstract string ViewFile { get; }
        
        public string Label { get; set; }

        public int Order { get; set; }

        public bool ReadOnly { get; set; }
    }
}
