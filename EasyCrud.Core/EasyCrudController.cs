using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EasyCrud.Model.Database;
using EasyCrud.Workflow;

namespace EasyCrud.Core
{
    public class EasyCrudController : Controller  
    {
        public ActionResult Index(string assemblyName, string contextName)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/Index.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName));
        }

        public ActionResult DbSetList(string assemblyName, string contextName, string dbSet)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/List.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName, dbSet));
        }

        public ActionResult DbSetEdit(string assemblyName, string contextName, string dbSet, int id)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/Edit.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName, dbSet, id));
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}
