using AutoMapper;
using ObajuStore.Common.Helpers;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Core;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ObajuStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _categoryService;

        public ProductController(IProductService productService,
            IProductCategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Details(long id)
        {
            try
            {
                var product = _productService.GetByID(id);
                var productViewModel = Mapper.Map<Product, ProductViewModel>(product);
                var images = productViewModel.MoreImages;
                var moreImages = new List<string>();
                if (!string.IsNullOrEmpty(images))
                {
                    var xImages = XElement.Parse(images);
                    foreach (var item in xImages.Elements())
                    {
                        moreImages.Add(item.Value);
                    }
                }

                ViewBag.MoreImages = moreImages;
                var relatedProducts = _productService.GetRelatedProducts(id, 5);
                ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);

                return View(productViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult ProductByCategory(int id, int page = 1, int brandId = 0)
        {
            int pageSize = PaginationHelper.PageSize();
            int totalRow = 0;
            var products = _productService.GetByCategoryIDPaging(id, brandId, page, pageSize, "", out totalRow);
            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            var categoryModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(_categoryService.GetByID(id));
            ViewBag.Category = categoryModel;
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
    }
}