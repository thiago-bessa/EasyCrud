using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;

namespace EasyCrud.Model.ViewData
{
    public class FieldViewData
    {
        public string Label { get; set; }
        public string ViewFile { get; set; }
        public int Order { get; set; }
        public bool ReadOnly { get; set; }
        public string Column { get; set; }
        public object Value { get; set; }
    }
}
