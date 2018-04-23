using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class PropertyConfigurationException : Exception
    {
        public PropertyConfigurationException(string message)
            : base(message)
        {

        }
    }
}
