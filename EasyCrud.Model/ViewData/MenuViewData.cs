using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Info;

namespace EasyCrud.Model.ViewData
{
    public class MenuViewData
    {
        public string RouteAlias { get; set; }
        public IEnumerable<MenuItemViewData> MenuItems { get; set; }

        public MenuViewData(DbContextInfo contextInfo, string routeAlias, string currentRepositoryName)
        {
            RouteAlias = routeAlias;
            MenuItems = contextInfo.GeAlltRepositories().Select(repository => new MenuItemViewData(repository, currentRepositoryName));
        }
    }
}
