using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Exceptions
{
    public class DbSetNotFoundException : Exception
    {
        public DbSetNotFoundException(string contextName, string dbSet)
            : base(GetMessage(contextName, dbSet))
        {

        }

        private static string GetMessage(string contextName, string dbSet)
        {
            return $"'{dbSet}' was not found in '{contextName}'";
        }
    }
}
