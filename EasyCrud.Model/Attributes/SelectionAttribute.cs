using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Enums;

namespace EasyCrud.Model.Attributes
{
    public class SelectionAttribute : BaseFieldAttribute
    {
        public SelectionType Type { get; set; }

        public override string ViewFile
        {
            get
            {
                switch (Type)
                {
                    case SelectionType.Modal:
                        return "selectionModal.cshtml";

                    default:
                        return "selection.cshtml";
                }
            }
        }
    }
}
