using AutoMapper;
using ObajuStore.Common;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Extensions;
using ObajuStore.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class ApplicationRoleController : BaseController
    {
        private IApplicationRoleService _roleService;

        public ApplicationRoleController(IApplicationRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: AdminObajuStore/ApplicationRole
        public ActionResult Index(string filter = null)
        {
            ViewBag.Filter = filter;

            var roles = _roleService.GetAll(filter);
            var model = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(roles);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = CommonConstants.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.ADMIN)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole();
                role.UpdateApplicationRole(model);
                _roleService.Add(role);
                _roleService.Save();
                SetAlert("Thêm thành công", "success");

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = CommonConstants.ADMIN)]
        public ActionResult Edit(string id)
        {
            if (ModelState.IsValid)
            {
                var role = _roleService.FindById(id);
                var model = Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(role);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.ADMIN)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleService.FindById(model.Id);
                role.UpdateApplicationRole(model, "update");
                _roleService.Update(role);
                _roleService.Save();

                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập nhật không thành công. Vui lòng thử lại!");
            return View(model);
        }

        [Authorize(Roles = CommonConstants.ADMIN)]
        public JsonResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _roleService.Delete(id);
                SetAlert("Xóa thành công", "success");
                return Json(new
                {
                    status = true
                });
            }
            SetAlert("Xóa không thành công", "error");
            return Json(new
            {
                status = false
            });
        }
    }
}