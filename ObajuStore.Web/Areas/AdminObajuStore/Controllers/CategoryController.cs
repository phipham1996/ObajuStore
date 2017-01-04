using AutoMapper;
using ObajuStore.Common;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Infrastructure.Extensions;
using ObajuStore.Web.Infrastructure.Helpers;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class CategoryController : BaseController
    {
        private IProductCategoryService _categoryService;

        public CategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: AdminObajuStore/Category

        public ActionResult Index(string q = null, int page = 1)
        {
            ViewBag.Filter = q;
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var categories = _categoryService.GetAllPaging(q, page, pageSize, out totalRow, true);
            var model = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(categories);

            var pagination = new PaginationSet<ProductCategoryViewModel>()
            {
                TotalCount = totalRow,
                TotalPages = PaginationHelper.TotalPages(totalRow, pageSize),
                Items = model,
                Page = page,
                MaxPage = PaginationHelper.MaxPage()
            };
            return View(pagination);
        }

        // GET: AdminObajuStore/Category/Details/5
        [Authorize(Roles = CommonConstants.ADMIN)]
        public ActionResult Details(int id)
        {
            var category = _categoryService.GetByID(id);
            var model = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            return View(model);
        }

        // GET: AdminObajuStore/Category/Create
        public ActionResult Create()
        {
            var model = new ProductCategoryViewModel();
            PrepareProductCategoriesModel(model);
            return View(model);
        }

        // POST: AdminObajuStore/Category/Create
        [HttpPost]
        [Authorize(Roles = CommonConstants.ADMIN)]
        public ActionResult Create(ProductCategoryViewModel model)
        {
            try
            {
                var category = new ProductCategory();
                category.UpdateProductCategory(model);
                _categoryService.Add(category);
                _categoryService.SaveChanges();
                SetAlert("Thêm danh mục thành công", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = CommonConstants.ADMIN)]
        // GET: AdminObajuStore/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetByID(id);
            var model = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            PrepareProductCategoriesModel(model);
            return View(model);
        }

        // POST: AdminObajuStore/Category/Edit/5
        [HttpPost]
        [Authorize(Roles = CommonConstants.ADMIN)]
        public ActionResult Edit(int id, ProductCategoryViewModel model)
        {
            try
            {
                var category = _categoryService.GetByID(model.ID);
                category.UpdateProductCategory(model);
                _categoryService.Update(category);
                _categoryService.SaveChanges();
                SetAlert("Cập nhật danh mục thành công", "success");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = CommonConstants.ADMIN)]
        // GET: AdminObajuStore/Category/Delete/5
        public JsonResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                _categoryService.SaveChanges();
                SetAlert("Xóa danh mục thành công", "success");
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

        #region Helpers

        [NonAction]
        protected virtual void PrepareProductCategoriesModel(ProductCategoryViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.ParentList == null)
                model.ParentList = new List<SelectListItem>();
            model.ParentList.Add(new SelectListItem
            {
                Text = "Chưa chọn danh mục",
                Value = ""
            });
            var categories = SelectListHelper.GetCategoryList(_categoryService);
            foreach (var c in categories)
                model.ParentList.Add(c);
        }

        public void SetViewBag(int? selectedID = null)
        {
            ViewBag.ParentID = new SelectList(_categoryService.GetAll(), "ParentID", "Name", selectedID);
        }

        #endregion Helpers
    }
}