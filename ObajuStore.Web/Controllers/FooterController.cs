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
    public class FooterController : Controller
    {
        private IProductCategoryService _productCategory;

        public FooterController(IProductCategoryService productCategory)
        {
            _productCategory = productCategory;
        }

        [ChildActionOnly]
        public PartialViewResult TopCategories()
        {
            var parents = _productCategory.GetParentProductCategory();
            var parentsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(parents);
            return PartialView("_TopCategories", parentsModel);
        }
    }
}