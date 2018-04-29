using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Interfaces;

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

        public IPageWorkflow GetPageWorkflow(string assemblyName, string contextName)
        {
            return new PageWorkflow(assemblyName, contextName);
        }

        public IUserWorkflow GetUserWorkflow()
        {
            var userRepository = _repositoryFactory.GetUserRepository(_crudContext);
            return new UserWorkflow(userRepository);
        }
    }
}
