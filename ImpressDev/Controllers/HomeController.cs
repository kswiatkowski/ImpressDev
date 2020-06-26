using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.Models;
using ImpressDev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class HomeController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public ActionResult Index()
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

            List<Book> bestsellers;
            if (cache.IsSet(Consts.BestsellersCacheKey))
            {
                bestsellers = cache.Get(Consts.BestsellersCacheKey) as List<Book>;
            }
            else
            {
                bestsellers = db.Books.Where(x => !x.Inaccessible && x.Bestseller).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellersCacheKey, bestsellers, 24);
            }


            var news = db.Books.Where(x => !x.Inaccessible).OrderByDescending(x => x.DateAdded).Take(24).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            var vm = new HomeViewModel { Categories = categories, Bestsellers = bestsellers, News = news };

            return View(vm);
        }
    }
}