using AutoMapper;
using ObajuStore.Common;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.App_Start;
using ObajuStore.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class ApplicationUserRoleController : BaseController
    {
        private ApplicationUserManager _userManager;
        private IApplicationUserService _userService;
        private IApplicationRoleService _roleService;

        public ApplicationUserRoleController(ApplicationUserManager userManager,
            IApplicationUserService userService, IApplicationRoleService roleService)
        {
            _userManager = userManager;
            _userService = userService;
            _roleService = roleService;
        }

        // GET: AdminObajuStore/ApplicationUserRole
        public ActionResult Index(string id)
        {
            var user = _userService.GetUserById(id);
            var model = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
            var roles = _roleService.GetAll();
            ViewBag.Roles = roles;

            var userRoles = _userManager.GetRolesAsync(id).Result.ToList();
            model.Roles = userRoles;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.ADMIN)]
        public async Task<JsonResult> UpdateRole(string role, string id)
        {
            try
            {
                var userRoles = _userManager.GetRolesAsync(id).Result.ToList();

                if (userRoles.Contains(role))
                {
                    var res = await _userManager.RemoveFromRoleAsync(id, role);
                    if (res.Succeeded)
                        SetAlert("Hủy quyền " + role + " thành công", "warning");
                    else
                        SetAlert("Hủy quyền " + role + " không thành công", "error");
                }
                else
                {
                    var res = await _userManager.AddToRoleAsync(id, role);
                    if (res.Succeeded)
                        SetAlert("Cấp quyền " + role + " thành công", "success");
                    else
                        SetAlert("Cấp quyền " + role + " không thành công", "error");
                }

                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}