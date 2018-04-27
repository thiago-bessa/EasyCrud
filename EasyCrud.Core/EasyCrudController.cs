using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EasyCrud.Core.Translation;
using EasyCrud.Model.Database;
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
            var assemblyName = context.Assembly.FullName;
            var contextName = context.FullName;

            routes.MapRoute(
                name: "EasyCrud Index",
                url: routeAlias,
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "Index",
                    assemblyName,
                    contextName
                }
            );

            routes.MapRoute(
                name: "EasyCrud Repository List",
                url: routeAlias + "/{repositoryName}",
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "RepositoryList",
                    assemblyName,
                    contextName,
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
                    assemblyName,
                    contextName,
                    repositoryName = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );
        }
        #endregion

        #region Actions

        public ActionResult Index(string assemblyName, string contextName)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/Index.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName));
        }

        public ActionResult RepositoryList(string assemblyName, string contextName, string repositoryName)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/List.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName, repositoryName));
        }

        public ActionResult RepositoryEdit(string assemblyName, string contextName, string repositoryName, int id)
        {
            var pageWorkflow = new PageWorkflow();
            return View("~/Views/EasyCrud/Edit.cshtml", pageWorkflow.GetPageViewData(assemblyName, contextName, repositoryName, id));
        }

        #endregion
    }
}
