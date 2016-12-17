using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore
{
    public class AdminObajuStoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminObajuStore";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminObajuStore_default",
                "AdminObajuStore/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ObajuStore.Web.Areas.AdminObajuStore.Controllers" }
            );
        }
    }
}