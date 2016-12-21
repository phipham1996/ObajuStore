using AutoMapper;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.App_Start;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class ApplicationUserController : BaseController
    {
        #region ctor

        private ICommonService _commonService;
        private IApplicationUserService _appUser;
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;

        public ApplicationUserController(
            IApplicationGroupService appGroupService,
            IApplicationUserService appUser,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService,
            ICommonService commonService)
        {
            _appRoleService = appRoleService;
            _appUser = appUser;
            _appGroupService = appGroupService;
            _userManager = userManager;
            _commonService = commonService;
        }

        #endregion ctor

        // GET: AdminObajuStore/ApplicationUser
        public ActionResult Index(string filter = null, int pageIndex = 1)
        {
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var users = _appUser.GetUserListPaging(pageIndex, pageSize, filter, out totalRow);
            var model = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(users);

            var pagination = new PaginationSet<ApplicationUserViewModel>()
            {
                TotalCount = totalRow,
                TotalPages = PaginationHelper.TotalPages(totalRow, pageSize),
                Items = model,
                Page = pageIndex,
                MaxPage = PaginationHelper.MaxPage()
            };
            return View(pagination);
        }
    }
}