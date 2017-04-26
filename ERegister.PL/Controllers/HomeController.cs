using System.Web.Mvc;

namespace ERegister.PL.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
