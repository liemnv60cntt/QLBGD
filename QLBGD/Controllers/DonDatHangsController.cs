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
    public class DonDatHangsController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: DonDatHangs
        public ActionResult Index()
        {
            var donDatHangs = db.DonDatHangs.Include(d => d.KhachHang).Include(d => d.NhanVien).Include(d => d.DonViVanChuyen);
            return View(donDatHangs.ToList());
        }

        // GET: DonDatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // GET: DonDatHangs/Create
    }
}
