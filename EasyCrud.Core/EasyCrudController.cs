using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EasyCrud.Core.Translation;
using EasyCrud.Data;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;
using EasyCrud.Model.Workflow;
using EasyCrud.Workflow;

namespace EasyCrud.Core
{
    [TranslationFilter]
    public class EasyCrudController : Controller  
    {
        #region SetUp

        public static void SetUp(Type context, RouteCollection routes, string routeAlias = "EasyCrud")
        {
            RegisterRoutes(context, routes, routeAlias);
        }

        private static void RegisterRoutes(Type context, RouteCollection routes, string routeAlias)
        {
            var parameters = new WorkflowParameters
            {
                AssemblyName = context.Assembly.FullName,
                ContextName = context.FullName,
                RouteAlias = routeAlias
            };

            routes.MapRoute(
                name: "EasyCrud Index",
                url: routeAlias,
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "Index",
                    parameters
                }
            );

            routes.MapRoute(
                name: "EasyCrud Repository List",
                url: routeAlias + "/{repositoryName}",
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "RepositoryList",
                    parameters,
                    repositoryName = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "EasyCrud Repository Edit",
                url: routeAlias + "/{repositoryName}/{id}",
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "RepositoryEdit",
                    parameters,
                    repositoryName = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );
        }
        #endregion

        #region Actions

        public ActionResult Index(WorkflowParameters parameters)
        { 
            return GetPageViewData(parameters, "~/Views/EasyCrud/Index.cshtml", pageWorkflow => pageWorkflow.GetPageViewData());
        }

        public ActionResult RepositoryList(WorkflowParameters parameters, string repositoryName)
        {
            return GetPageViewData(parameters, "~/Views/EasyCrud/List.cshtml", pageWorkflow => pageWorkflow.GetPageViewData(repositoryName));
        }

        public ActionResult RepositoryEdit(WorkflowParameters parameters, string repositoryName, string id)
        {
            return GetPageViewData(parameters, "~/Views/EasyCrud/Edit.cshtml", pageWorkflow => pageWorkflow.GetPageViewData(repositoryName, id));
        }

        #endregion

        #region Private Methods

        private ActionResult GetPageViewData(WorkflowParameters parameters, string viewName, Func<IPageWorkflow, PageViewData> getPageViewData)
        {
            var repositoryFactory = new RepositoryFactory();
            var workflowFactory = new WorkflowFactory(repositoryFactory);
            var pageWorkflow = workflowFactory.GetPageWorkflow(parameters);
            return View(viewName, getPageViewData.Invoke(pageWorkflow));
        }

        #endregion
    }
}
