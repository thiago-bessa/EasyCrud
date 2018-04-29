using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Info;

namespace EasyCrud.Model.ViewData
{
    public class MenuItemViewData
    {
        public string Label { get; set; }
        public string Repository { get; set; }
        public int Order { get; set; }

        public MenuItemViewData(RepositoryInfo repositoryInfo)
        {
            Label = repositoryInfo.Label;
            Repository = repositoryInfo.Name;
            Order = repositoryInfo.Order;
        }
    }
}
