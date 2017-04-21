using System.Web.Mvc;

namespace ERegister.Controllers
{
    [RoutePrefix("api/Home")]
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
