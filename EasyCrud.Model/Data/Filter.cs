using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Data
{
    public class Filter
    {
        public string Column { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }
}