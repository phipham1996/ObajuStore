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

        public ProductCategoryController(IProductCategoryService productCategory)
        {
            _productCategory = productCategory;
        }

        // GET: ProductCategory
        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var parents = _productCategory.GetParentProductCategory();
            var parentsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(parents);
            return PartialView("_ProductCategory", parentsModel);
        }

    }
}