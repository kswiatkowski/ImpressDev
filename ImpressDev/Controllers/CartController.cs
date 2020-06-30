using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpressDev.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartMenager;
        private ISessionManager sessionMenager { get; set; }
        private ImpressDevContext db = new ImpressDevContext();

        public CartController()
        {
            sessionMenager = new SessionManager();
            cartMenager = new CartManager(sessionMenager, db);
        }

        public ActionResult Index()
        {
            var cartItems = cartMenager.GetCart();
            var totalPrice = cartMenager.GetCartPrice();

            CartViewModel vm = new CartViewModel() { CartItems = cartItems, TotalPrice = totalPrice };
            return View(vm);
        }

        public ActionResult AddToCart(int bookId)
        {
            cartMenager.AddToCart(bookId);
            return RedirectToAction("Index");
        }

        public int GetCartQuantity()
        {
            return cartMenager.GetCartQuantity();
        }

        public ActionResult RemoveFromCart(int bookId)
        {
            int bookQuantity = cartMenager.RemoveFromCart(bookId);
            int cartQuantity = cartMenager.GetCartQuantity();
            decimal cartPrice = cartMenager.GetCartPrice();

            var result = new CartRemoveViewModel() { RemoveBookId = bookId, RemoveBookQuantity = bookQuantity, CartItemsQuantity = cartQuantity, CartTotalPrice = cartPrice };
            return Json(result);
        }
    }
}