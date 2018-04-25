using System.Web.Mvc;

namespace EasyCrud.SampleWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return
            View();
        }
    }
}