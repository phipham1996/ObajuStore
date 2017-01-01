using AutoMapper;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Infrastructure.Extensions;
using ObajuStore.Web.Infrastructure.Helpers;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private IBrandService _brandService;
        private IProductCategoryService _categoryService;

        public ProductController(IProductService productService,
            IProductCategoryService categoryService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        // GET: AdminObajuStore/Product
        public ActionResult Index(string q = null, int page = 1, int brandId = 0)
        {
            ViewBag.Filter = q;
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var products = _productService.GetIncludeBrandPaging(q, page, brandId, pageSize, out totalRow);
            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            var pagination = new PaginationSet<ProductViewModel>()
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
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            PrepareProductCategoriesModel(model);
            ViewBag.Brands = new SelectList(_brandService.GetAll(""), "ID", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.UpdateProduct(model);
                product.CreatedBy = User.Identity.Name;
                _productService.Add(product);
                _productService.SaveChanges();

                SetAlert("Đã thêm " + model.Name + " vào danh sách sản phẩm", "success");
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = _productService.GetByID(id);
            var model = Mapper.Map<Product, ProductViewModel>(product);
            PrepareProductCategoriesModel(model);
            ViewBag.Brands = new SelectList(_brandService.GetAll(""), "ID", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetByID(model.ID);
                product.UpdateProduct(model);
                product.UpdatedBy = User.Identity.Name;
                product.UpdatedDate = DateTime.Now;
                _productService.Update(product);
                _productService.SaveChanges();

                SetAlert("Cập nhật " + model.Name + " thành công", "success");
                return RedirectToAction("Index");
            }
            return View();
        }

        public JsonResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                _productService.SaveChanges();
                SetAlert("Xóa sản phẩm thành công", "success");
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
        protected virtual void PrepareProductCategoriesModel(ProductViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (model.CategoryList == null)
                model.CategoryList = new List<SelectListItem>();
            model.CategoryList.Add(new SelectListItem
            {
                Text = "Chưa chọn danh mục",
                Value = ""
            });
            var categories = SelectListHelper.GetCategoryList(_categoryService);
            foreach (var c in categories)
                model.CategoryList.Add(c);
        }

        #endregion Helpers
    }
}