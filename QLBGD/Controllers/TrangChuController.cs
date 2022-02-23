using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLBGD.Models;

namespace QLBGD.Controllers
{
    public class TrangChuController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: TrangChu
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhanHieu);
            return View(sanPhams);
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
