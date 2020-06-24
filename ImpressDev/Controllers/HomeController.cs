using ImpressDev.DAL;
using ImpressDev.Models;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class HomeController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}