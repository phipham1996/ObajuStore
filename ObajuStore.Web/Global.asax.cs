using ObajuStore.Web.Mappings;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ObajuStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            CultureInfo culture;
            if (Thread.CurrentThread.CurrentCulture.Name == "vi-VN")
                culture = CultureInfo.CreateSpecificCulture("vi-VN");
            else
                culture = CultureInfo.CreateSpecificCulture("vi-VN");

            CultureInfo.DefaultThreadCurrentCulture = culture;
        }
    }
}