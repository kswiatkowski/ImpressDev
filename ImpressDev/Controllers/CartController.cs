using ImpressDev.App_Start;
using ImpressDev.DAL;
using ImpressDev.Infrastructure;
using ImpressDev.Models;
using ImpressDev.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Pay()
        {
            if(Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    Name = user.UserData.Name,
                    Surname = user.UserData.Surname,
                    Street = user.UserData.Street,
                    City = user.UserData.City,
                    PostalCode = user.UserData.PostalCode,
                    Email = user.UserData.Email,
                    PhoneNumber = user.UserData.Phone
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Pay", "Cart") });
            }
        }

        private ApplicationSignInManager _signInManager;
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

        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if(ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = cartMenager.CreateNewOrder(orderDetails, userId);

                //update user data
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                cartMenager.CleanCart();

                return RedirectToAction("ConfirmOrder");
            }
            else
            {
                return View(orderDetails);
            }
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }
    }
}