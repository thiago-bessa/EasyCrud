using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class TypeIsNotDbContextException : Exception
    {
        public TypeIsNotDbContextException(string contextName)
            : base($"'{contextName}' is not a DbContext.")
        {

        }
    }
}
