using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Info
{
    public class ColumnInfo
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public string ViewFile { get; set; }
        public bool ReadOnly { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}
