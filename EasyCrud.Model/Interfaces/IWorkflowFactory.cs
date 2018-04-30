using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Workflow;

namespace EasyCrud.Model.Interfaces
{
    public interface IWorkflowFactory
    {
        IPageWorkflow GetPageWorkflow(WorkflowParameters parameters);
        IUserWorkflow GetUserWorkflow();
    }
}
