using ImpressDev.DAL;
using ImpressDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class CatalogController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public ActionResult Index(string categoryName = null)
        {
            var books = new List<Book>();
            if (categoryName == null)
            {
                books = db.Books.Where(x => !x.Inaccessible).OrderBy(x => Guid.NewGuid()).Take(9).ToList();
            }
            else
            {
                books = db.Books.Where(x => !x.Inaccessible && x.Category.Name.ToLower() == categoryName).ToList();
            }
            return View(books);
        }

        [ChildActionOnly]
        public ActionResult GetCategories()
        {
            var categories = db.Categories.ToList();
            return PartialView("_PartialCategoriesLeft", categories);
        }
    }
}