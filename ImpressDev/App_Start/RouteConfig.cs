using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImpressDev
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Catalog",
                url: "katalog/{categoryName}",
                defaults: new { controller = "Catalog", action = "Index" });

            routes.MapRoute(
               name: "Details",
               url: "katalog/szczegoly/{bookId}",
               defaults: new { controller = "Catalog", action = "Details" });

            routes.MapRoute(
               name: "Cart",
               url: "koszyk",
               defaults: new { controller = "Cart", action = "Index" });

            routes.MapRoute(
               name: "Pay",
               url: "koszyk/podsumowanie",
               defaults: new { controller = "Cart", action = "Pay" });

            routes.MapRoute(
               name: "Confirm",
               url: "koszyk/potwierdzenie",
               defaults: new { controller = "Cart", action = "ConfirmOrder" });

            routes.MapRoute(
               name: "Login",
               url: "moje-konto/zaloguj",
               defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
               name: "Register",
               url: "moje-konto/zarejestruj",
               defaults: new { controller = "Account", action = "Register" });

            routes.MapRoute(
               name: "UserData",
               url: "moje-konto/edycja-danych",
               defaults: new { controller = "Manage", action = "Index" });

            routes.MapRoute(
               name: "OrdersList",
               url: "moje-konto/lista-zamowien",
               defaults: new { controller = "Manage", action = "OrdersList" });

            routes.MapRoute(
               name: "AddBook",
               url: "moje-konto/edycja-produktu",
               defaults: new { controller = "Manage", action = "AddBook" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
