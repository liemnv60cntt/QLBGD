using QLBGD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBGD.Areas.NhanViens.Controllers
{
    public class LoaiSanPhamNVController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: NhanViens/LoaiSanPhamNV
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }
    }
}