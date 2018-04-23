using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Enums;

namespace EasyCrud.Model.Attributes
{
    public class TextAttribute : BaseFieldAttribute
    {
        public TextType Type { get; set; }

        public override string ViewFile
        {
            get
            {
                switch (Type)
                {
                    case TextType.Html:
                        return "html.cshtml";

                    case TextType.TextArea:
                        return "textarea.cshtml";

                    default:
                        return "text.cshtml";
                }
            }
        }
    }
}
