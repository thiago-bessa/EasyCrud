using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.ViewData;

namespace EasyCrud.Model.Interfaces
{
    public interface IPageWorkflow
    {
        PageViewData GetPageViewData();
        PageViewData GetPageViewData(string repositoryName);
        PageViewData GetPageViewData(string repositoryName, string id);
    }
}
