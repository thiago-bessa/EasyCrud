using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EasyCrud.Core.Translation
{
    public class TranslationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var original = filterContext.HttpContext.Response.Filter;
            filterContext.HttpContext.Response.Filter = new TranslationStream(original);
        }
    }
}
