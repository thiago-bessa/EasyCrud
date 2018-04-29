using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string repositoryName, object id)
            : base($"No Entity with ID '{id}' was found at '{repositoryName}'.")
        {

        }
    }
}
