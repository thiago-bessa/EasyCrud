using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;

namespace EasyCrud.Model.ViewData
{
    public class ComponentViewData
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public List<FieldViewData> Fields { get; set; }
        public List<EntityViewData> Entities { get; set; }

        public ComponentViewData()
        {
            Fields = new List<FieldViewData>();
        }
    }
}
