using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyCrud.Core
{
    public static class EasyCrud
    {
        public static void SetUp(Type context, RouteCollection routes, string routeAlias = "EasyCrud")
        {
            var assemblyName = context.Assembly.FullName;
            var contextName = context.FullName;

            RegisterRoutes(routes, routeAlias, assemblyName, contextName);
        }

        private static void RegisterRoutes(RouteCollection routes, string routeAlias, string assemblyName, string contextName)
        {
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
                name: "EasyCrud DbSet List",
                url: routeAlias + "/{dbSet}",
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "DbSetList",
                    assemblyName,
                    contextName,
                    dbSet = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "EasyCrud DbSet Edit",
                url: routeAlias + "/{dbSet}/{id}",
                defaults: new
                {
                    controller = "EasyCrud",
                    action = "DbSetEdit",
                    assemblyName,
                    contextName,
                    dbSet = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
