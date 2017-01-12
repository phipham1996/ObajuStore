using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Controllers
{
    public class ProductDesignController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DesignDocument()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult WearOnline(string imgUrl)
        {
            var alertString = "";
            if (!Request.IsAuthenticated)
            {
                alertString = "Bạn phải đăng nhập để thực hiện tính năng này.";
                ViewBag.AlertString = alertString;
                return View("Index");
            }

            if (!string.IsNullOrEmpty(imgUrl))
                Session["imgUrl"] = imgUrl;
            return View();
        }
    }
}