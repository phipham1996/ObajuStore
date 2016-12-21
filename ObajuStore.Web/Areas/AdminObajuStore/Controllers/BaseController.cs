using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    [Authorize(Roles = "ADMIN, MOD, MAN")]
    public class BaseController : Controller
    {
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else
                if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
                    if (type == "error")
            {
                TempData["AlertType"] = "alert-error";
            }
        }
    }
}