using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Data
{
    public class Criteria
    {
        public List<string> Columns { get; set; }
        public List<Filter> Filters { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
