﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBGD.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if (Session["Staff"] == null || Session["Staff"].ToString() == "" || (bool)Session["QuanLy"] != true)
            {
                return RedirectToAction("Index", "LoginA", new { area = "Admin" });
            }

            return View();
        }
    }
}