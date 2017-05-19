using System.Web.Mvc;

namespace ERegister.PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult AddGroup()
        {
            ViewBag.Title = "Add Group";
            return View();
        }
        public ActionResult AddSubject()
        {
            ViewBag.Title = "Add Group";
            return View();
        }
    }
}
