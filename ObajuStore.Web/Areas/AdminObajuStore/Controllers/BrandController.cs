using AutoMapper;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Infrastructure.Extensions;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class BrandController : BaseController
    {
        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        // GET: AdminObajuStore/Brand
        public ActionResult Index(string q = null, int page = 1)
        {
            ViewBag.Filter = q;
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var brands = _brandService.GetAllPaging(q, page, pageSize, out totalRow);
            var model = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands);

            var pagination = new PaginationSet<BrandViewModel>()
            {
                TotalCount = totalRow,
                TotalPages = PaginationHelper.TotalPages(totalRow, pageSize),
                Items = model,
                Page = page,
                MaxPage = PaginationHelper.MaxPage()
            };
            return View(pagination);
        }

        // GET: AdminObajuStore/Brand/Details/5
        public ActionResult Details(int id)
        {
            var brand = _brandService.GetByID(id);
            var model = Mapper.Map<Brand, BrandViewModel>(brand);
            return View(model);
        }

        // GET: AdminObajuStore/Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminObajuStore/Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandViewModel model)
        {
            try
            {
                var brand = new Brand();
                brand.UpdateBrand(model);
                brand.CreatedBy = User.Identity.Name;
                brand.CreatedDate = DateTime.Now;
                _brandService.Add(brand);
                _brandService.SaveChanges();

                SetAlert("Đã thêm " + model.Name + " vào danh sách thương hiệu", "success");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("create-brand-err", ex.Message);
                return View();
            }
        }

        // GET: AdminObajuStore/Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = _brandService.GetByID(id);
            var model = Mapper.Map<Brand, BrandViewModel>(brand);
            return View(model);
        }

        // POST: AdminObajuStore/Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BrandViewModel model)
        {
            try
            {
                var brand = _brandService.GetByID(id);

                brand.UpdateBrand(model);

                brand.UpdatedBy = User.Identity.Name;
                brand.UpdatedDate = DateTime.Now;
                _brandService.Update(brand);
                _brandService.SaveChanges();
                SetAlert("Cập nhật " + model.Name + " thành công", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("edit-brand-err", ex.Message);
                return View();
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                _brandService.Delete(id);
                SetAlert("Xóa thành công", "success");
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("delete-brand-err", ex.Message);
                return Json(new
                {
                    status = false
                });
            }
        }

    }
}
