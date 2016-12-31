using AutoMapper;
using ObajuStore.Common;
using ObajuStore.Common.Exceptions;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.App_Start;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Infrastructure.Extensions;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class ApplicationUserController : BaseController
    {
        #region ctor

        private ICommonService _commonService;
        private IApplicationUserService _appUser;
        private ApplicationUserManager _userManager;
        private IApplicationRoleService _appRoleService;

        public ApplicationUserController(
            IApplicationUserService appUser,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService,
            ICommonService commonService)
        {
            _appRoleService = appRoleService;
            _appUser = appUser;
            _userManager = userManager;
            _commonService = commonService;
        }

        #endregion ctor

        // GET: AdminObajuStore/ApplicationUser
        public ActionResult Index(string filter = null, int page = 1)
        {
            ViewBag.Filter = filter;
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var users = _appUser.GetUserListPaging(page, pageSize, filter, out totalRow);
            var model = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(users);

            var pagination = new PaginationSet<ApplicationUserViewModel>()
            {
                TotalCount = totalRow,
                TotalPages = PaginationHelper.TotalPages(totalRow, pageSize),
                Items = model,
                Page = page,
                MaxPage = PaginationHelper.MaxPage()
            };
            return View(pagination);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN, MAN")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN, MAN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserName = model.Email;
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(model);
                }
                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    return View(model);
                }
                var user = new ApplicationUser();

                user.UpdateUser(model);
                user.IsDeleted = false;
                user.CreatedBy = User.Identity.Name;
                user.CreatedDate = DateTime.Now;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                user.TwoFactorEnabled = false;
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;
                try
                {
                    user.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var userModel = await _userManager.FindByEmailAsync(model.Email);
                        if (userModel != null)
                        {
                            await _userManager.AddToRolesAsync(userModel.Id, new string[] { CommonConstants.MEM });
                        }
                        SetAlert("Thêm mới " + model.FullName + " thành công.", "success");
                        Thread.Sleep(2000);
                        return RedirectToAction("Index");
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN, MAN")]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
            return View(userModel);
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN, MAN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(model.Id);
                try
                {
                    appUser.UpdateUser(model);
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        SetAlert("Cập nhât " + model.FullName + " thành công.", "success");
                        Thread.Sleep(2000);
                        return RedirectToAction("Index");
                    }
                    else
                        return View(model);
                }
                catch (NameDuplicatedException dex)
                {
                    ModelState.AddModelError("", dex.Message);
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Cập nhật không thành công. Vui lòng thử lại!");
            return View(model);
        }
        [Authorize(Roles = CommonConstants.ADMIN)]
        public async Task<JsonResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(id);
                appUser.IsDeleted = true;
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    SetAlert("Xóa " + appUser.FullName + " thành công.", "success");
                    Thread.Sleep(2000);
                    return Json(new
                    {
                        status = true
                    });
                }
            }
            return Json(new
            {
                status = false
            });
        }
    }
}