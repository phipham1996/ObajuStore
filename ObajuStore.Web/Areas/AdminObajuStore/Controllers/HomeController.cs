﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Areas.AdminObajuStore.Controllers
{
    public class HomeController : BaseController
    {
        // GET: AdminObajuStore/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}