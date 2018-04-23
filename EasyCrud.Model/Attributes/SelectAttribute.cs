using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Attributes
{
    public class SelectAttribute : BaseFieldAttribute
    {
        public override string ViewFile => "select.cshtml";
    }
}
