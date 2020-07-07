using ImpressDev.App_Start;
using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.Models;
using ImpressDev.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ImpressDevContext db = new ImpressDevContext();
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult OrdersList()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Order> userOrders;

            if (isAdmin)
            {
                userOrders = db.Orders.Include("OrderItems").OrderByDescending(x => x.DateAdded).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOrders = db.Orders.Where(x => x.UserId == userId).Include("OrderItems").OrderByDescending(x => x.DateAdded).ToArray();
            }
            return View(userOrders);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public OrderStatus ChangeOrderStatus(Order order)
        {
            Order orderToModify = db.Orders.Find(order.OrderId);
            orderToModify.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            return order.OrderStatus;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddBook (int? bookId, bool? confirm)
        {
            Book book;
            if (bookId.HasValue)
            {
                ViewBag.EditMode = true;
                book = db.Books.Find(bookId);
            }
            else
            {
                ViewBag.EditMode = false;
                book = new Book();
            }

            var result = new EditBookViewModel();
            result.Categories = db.Categories.ToList();
            result.Book = book;
            //confirm when [HttpPost]AddBook succeed
            result.Confirm = confirm;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddBook(EditBookViewModel model, HttpPostedFileBase file)
        {
            if (model.Book.BookId > 0)
            {
                // edit
                db.Entry(model.Book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddBook", new { confirm = true });
            }
            else
            {
                //add
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;
                        var path = Path.Combine(Server.MapPath(AppConfig.BookPhotoSourceFolder), filename);
                        file.SaveAs(path);

                        model.Book.PhotoSource = filename;
                        model.Book.DateAdded = DateTime.Now;

                        db.Books.Add(model.Book);
                        db.SaveChanges();

                        return RedirectToAction("AddBook", new { confirm = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    // in case of an error - the model will be forwarded, but without a category (to dropdown list)
                    var categories = db.Categories.ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }
        }
    }
}