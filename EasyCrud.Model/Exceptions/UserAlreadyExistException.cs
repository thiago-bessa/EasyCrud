using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException()
            : base("The is already another user with the same username.")
        {
        }
    }
}
