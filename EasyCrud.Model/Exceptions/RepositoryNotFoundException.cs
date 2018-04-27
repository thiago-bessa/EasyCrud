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
            : base(GetMessage(contextName, repositoryName))
        {

        }

        private static string GetMessage(string contextName, string repositoryName)
        {
            return $"'{repositoryName}' was not found in '{contextName}'";
        }
    }
}
