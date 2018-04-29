using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class ColumnNotFoundException : Exception
    {
        public ColumnNotFoundException(string columnName)
            : base($"Column '{columnName}' does not exist.")
        {

        }
    }
}
