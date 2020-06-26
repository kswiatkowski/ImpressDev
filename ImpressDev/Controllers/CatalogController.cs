using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}