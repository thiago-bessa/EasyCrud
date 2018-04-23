using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Attributes
{
    public class ImageAttribute : BaseFieldAttribute
    {
        public override string ViewFile => "image.cshtml";
    }
}
