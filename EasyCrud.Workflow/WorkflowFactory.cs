using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.Workflow;

namespace EasyCrud.Workflow
{
    public class WorkflowFactory : IWorkflowFactory
    {
        private readonly EasyCrudContext _crudContext;
        private readonly IRepositoryFactory _repositoryFactory;

        public WorkflowFactory(IRepositoryFactory repositoryFactory)
        {
            _crudContext = new EasyCrudContext();
            _repositoryFactory = repositoryFactory;
        }

        public IPageWorkflow GetPageWorkflow(WorkflowParameters parameters)
        {
            return new PageWorkflow(parameters);
        }

        public IUserWorkflow GetUserWorkflow()
        {
            var userRepository = _repositoryFactory.GetUserRepository(_crudContext);
            return new UserWorkflow(userRepository);
        }
    }
}
