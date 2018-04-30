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
        public bool IsActive { get; set; }

        public MenuItemViewData(RepositoryInfo repositoryInfo, string currentRepositoryName)
        {
            Label = repositoryInfo.Label;
            Repository = repositoryInfo.Name;
            Order = repositoryInfo.Order;
            IsActive = repositoryInfo.Name.Equals(currentRepositoryName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
