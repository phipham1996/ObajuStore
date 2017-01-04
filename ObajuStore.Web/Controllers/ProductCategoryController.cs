using AutoMapper;
using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategory;
        private IBrandService _brandCategory;

        public ProductCategoryController(IProductCategoryService productCategory,
            IBrandService brandCategory)
        {
            _productCategory = productCategory;
            _brandCategory = brandCategory;
        }

        // GET: ProductCategory
        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var parents = _productCategory.GetParentProductCategory();
            var parentsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(parents);
            return PartialView("_ProductCategory", parentsModel);
        }

        [ChildActionOnly]
        public PartialViewResult SidebarCategories()
        {
            var sidebarViewModel = new SidebarViewModel();
            var parents = _productCategory.GetParentProductCategory();
            var parentsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(parents);

            var brands = _brandCategory.GetActivedBrand("");
            var brandsModel = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands);
            sidebarViewModel.ProductCategories = parentsModel;
            sidebarViewModel.Brands = brandsModel;
            return PartialView("_SidebarCategories", sidebarViewModel);
        }

    }
}