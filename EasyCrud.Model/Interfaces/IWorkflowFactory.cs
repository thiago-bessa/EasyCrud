using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Interfaces
{
    public interface IWorkflowFactory
    {
        IPageWorkflow GetPageWorkflow(string assemblyName, string contextName);
        IUserWorkflow GetUserWorkflow();
    }
}
