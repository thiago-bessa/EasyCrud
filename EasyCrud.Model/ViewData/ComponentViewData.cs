using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.ViewData
{
    public class ComponentViewData
    {
        public List<FieldViewData> Fields { get; set; }

        public ComponentViewData()
        {
            Fields = new List<FieldViewData>();
        }
    }
}
