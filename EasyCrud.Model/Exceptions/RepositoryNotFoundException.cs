using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException(string contextName, string repositoryName)
            : base($"'{repositoryName}' was not found in '{contextName}'")
        {

        }
    }
}
