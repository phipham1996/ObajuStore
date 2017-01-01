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
                name: "Manage",
                url: "tai-khoan.htm",
                defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional },
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