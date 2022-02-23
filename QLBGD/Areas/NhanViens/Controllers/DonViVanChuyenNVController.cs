using QLBGD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBGD.Areas.NhanViens.Controllers
{
    public class DonViVanChuyenNVController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: NhanViens/DonViVanChuyenNV
        public ActionResult Index()
        {
            return View(db.DonViVanChuyens.ToList());
        }
    }
}