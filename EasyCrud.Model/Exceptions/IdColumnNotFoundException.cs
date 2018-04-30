using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class IdColumnNotFoundException : Exception
    {
        public IdColumnNotFoundException()
            : base("There is no 'Id' property of type Int32")
        {

        }
    }
}
