using ImpressDev.ViewModels;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid == false)
                return View(model);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register( RegisterViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}