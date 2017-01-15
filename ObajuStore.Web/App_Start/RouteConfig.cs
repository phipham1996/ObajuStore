using System.Web.Mvc;
using System.Web.Routing;

namespace ObajuStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Shopping Cart Index",
             url: "gio-hang-cua-ban.htm",
             defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "ObajuStore.Web.Controllers" }
         );

            routes.MapRoute(
                name: "Manage",
                url: "tai-khoan.htm",
                defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ObajuStore.Web.Controllers" }
            );

            routes.MapRoute(
              name: "Product Design",
              url: "thiet-ke-ao.htm",
              defaults: new { controller = "ProductDesign", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "ObajuStore.Web.Controllers" }
          );
            routes.MapRoute(
              name: "AR_Controller",
              url: "thu-do-online.htm",
              defaults: new { controller = "ProductDesign", action = "WearOnline", id = UrlParameter.Optional },
              namespaces: new string[] { "ObajuStore.Web.Controllers" }
          );
            routes.MapRoute(
              name: "Product Design Doc",
              url: "huong-dan-thiet-ke.htm",
              defaults: new { controller = "ProductDesign", action = "DesignDocument", id = UrlParameter.Optional },
              namespaces: new string[] { "ObajuStore.Web.Controllers" }
          );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he.htm",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ObajuStore.Web.Controllers" }
           );

            routes.MapRoute(
               name: "About",
               url: "gioi-thieu.htm",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ObajuStore.Web.Controllers" }
           );

            routes.MapRoute(
               name: "Product",
               url: "danh-muc/{alias}-{id}.htm",
               defaults: new { controller = "Product", action = "ProductByCategory", id = UrlParameter.Optional },
               namespaces: new string[] { "ObajuStore.Web.Controllers" }
           );

            routes.MapRoute(
              name: "Product Details",
              url: "san-pham/{alias}-{id}.htm",
              defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional },
              namespaces: new string[] { "ObajuStore.Web.Controllers" }
          );

            routes.MapRoute(
                name: "Login",
                url: "login.htm",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "ObajuStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register.htm",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "ObajuStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ObajuStore.Web.Controllers" }
            );
        }
    }
}