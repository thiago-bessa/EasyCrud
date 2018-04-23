using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Enums;

namespace EasyCrud.Model.Attributes
{
    public class DateTimeAttribute : BaseFieldAttribute
    {
        public DateTimeType Type { get; set; }

        public override string ViewFile => "datetime.cshtml";
    }
}
