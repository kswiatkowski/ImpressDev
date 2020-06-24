using ImpressDev.DAL;
using ImpressDev.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class HomeController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            var bestsellers = db.Books.Where(x => !x.Inaccessible && x.Bestseller).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            var news = db.Books.Where(x => !x.Inaccessible).OrderByDescending(x => x.DateAdded).Take(24).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            var vm = new HomeViewModel { Categories = categories, Bestsellers = bestsellers, News = news };

            return View(vm);
        }
    }
}