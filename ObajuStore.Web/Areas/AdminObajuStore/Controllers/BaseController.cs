using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    [Authorize(Roles = "ADMIN, MOD")]
    public class BaseController : Controller
    {
    }
}