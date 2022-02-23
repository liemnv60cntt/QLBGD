﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBGD.Areas.NhanViens.Controllers
{
    [Authorize]
    public class NhanVienHomeController : Controller
    {
        // GET: NhanVien/NhanVienHome
        public ActionResult Index()
        {
            if (Session["Staff"] == null || Session["Staff"].ToString() == "" || (bool)Session["QuanLy"] != false)
            {
                return RedirectToAction("Index", "LoginA", new { area = "Admin" });
            }
            return View();
        }
    }
}