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
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductHotThisWeek()
        {
            var hotProduct = _productService.GetHot(10);
            var hotProductModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProduct);

            return PartialView("_ProductHotThisWeek", hotProductModel);
        }

        public PartialViewResult GetInspired()
        {
            var getInspired = _productService.GetInspired();
            var getInspiredModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(getInspired);

            return PartialView("_getInspired", getInspiredModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}