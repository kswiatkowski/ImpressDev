using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class CatalogController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public ActionResult Index(string categoryName = null, string searchQuery = null)
        {
            if (categoryName == null)
            {
                categoryName = (db.Categories.Find(1)).Name;
            }

            var category = db.Categories.Include("Books").Where(x => x.Name.ToLower() == categoryName).Single();

            var books = category.Books.Where(x => (searchQuery == null ||
                                  x.Title.ToLower().Contains(searchQuery.ToLower()) ||
                                  x.SubTitle.ToLower().Contains(searchQuery.ToLower()) ||
                                  x.Level.ToLower().Contains(searchQuery.ToLower()) ||
                                  (searchQuery == x.Title + " " + x.SubTitle + " " + x.Level)) &&
                                  !x.Inaccessible);
            
            if (Request.IsAjaxRequest())
                return PartialView("_PartialBooksList", books);
            else
                return View(books);
        }

        [ChildActionOnly]
        public ActionResult GetCategories()
        {
            ICacheProvider cache = new DefaultCacheProvider();

            List<Category> categories;
            if (cache.IsSet(Consts.CategoriesCacheKey))
            {
                categories = cache.Get(Consts.CategoriesCacheKey) as List<Category>;
            }
            else
            {
                categories = db.Categories.ToList();
                cache.Set(Consts.CategoriesCacheKey, categories, 24);
            }
            return PartialView("_PartialCategoriesLeft", categories);
        }

        public ActionResult Details(int bookId = 1)
        {
            var book = db.Books.Find(bookId);
            return View(book);
        }

        public ActionResult SearchTips(string term, string categoryName)
        {
            var category = db.Categories.Include("Books").Where(x => x.Name.ToLower() == categoryName).Single();
           
            var books = category.Books.Where(x => !x.Inaccessible && (x.Title.ToLower().Contains(term.ToLower()))
                                                            || (x.SubTitle.ToLower().Contains(term.ToLower()))
                                                            || (x.Level.ToLower().Contains(term.ToLower())))
                                                            .Take(3).Select(x => new { label = x.Title + " " + x.SubTitle + " " + x.Level });
            
            return Json(books, JsonRequestBehavior.AllowGet);
        }
    }
}