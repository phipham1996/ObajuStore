using ObajuStore.Common.Helpers;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Home()
        {
            var homePage = ConfigHelper.GetByKey("HomePage");
            return Redirect(homePage);
        }

        public ActionResult MyProfile()
        {
            var profile = ConfigHelper.GetByKey("Profile");
            return Redirect(profile);
        }
    }
}