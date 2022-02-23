﻿using QLBGD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLBGD.Areas.Admin.Controllers
{
    public class DonDatHangAController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: Admin/DonDatHangA
        public ActionResult Index(string maDDH = "", string maKH = "", string tinhTrang = "")
        {
            ViewBag.MaDDH = maDDH;
            ViewBag.MaKhachHang = maKH;

            int check1 = 1;
            int check2 = 2;
            int check3 = 3;
            ViewBag.TinhTrang = null;

            if (tinhTrang == "1")
            {
                check2 = 1;
                check3 = 1;
                ViewBag.TinhTrang = "1";
            }
            else if (tinhTrang == "2")
            {
                check1 = 2;
                check3 = 2;
                ViewBag.TinhTrang = "2";
            }
            else if (tinhTrang == "3")
            {
                check1 = 3;
                check2 = 3;
                ViewBag.TinhTrang = "3";
            }

            var donDatHangs = db.DonDatHangs.Where(ddh => ddh.MaDonDatHang.Contains(maDDH) &&
                                                          ddh.MaKhachHang.Contains(maKH) &&
                                                          (ddh.TinhTrang == check1 || ddh.TinhTrang == check2 || ddh.TinhTrang == check3 ));

            if (donDatHangs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";

            return View(donDatHangs.ToList());
        }

        // GET: Admin/DonDatHangA/Details/5
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

        // GET: Admin/DonDatHangA/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ho");
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "Ho");
            ViewBag.MaDonViVanChuyen = new SelectList(db.DonViVanChuyens, "MaDonVi", "TenDonVi");
            return View();
        }

        // POST: Admin/DonDatHangA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonDatHang,MaKhachHang,MaNhanVien,MaDonViVanChuyen,NgayDatHang,NgayGiaoHang,DiaChiGiao,TinhTrang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ho", donDatHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "Ho", donDatHang.MaNhanVien);
            ViewBag.MaDonViVanChuyen = new SelectList(db.DonViVanChuyens, "MaDonVi", "TenDonVi", donDatHang.MaDonViVanChuyen);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangA/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ho", donDatHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "Ho", donDatHang.MaNhanVien);
            ViewBag.MaDonViVanChuyen = new SelectList(db.DonViVanChuyens, "MaDonVi", "TenDonVi", donDatHang.MaDonViVanChuyen);
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonDatHang,MaKhachHang,MaNhanVien,MaDonViVanChuyen,NgayDatHang,NgayGiaoHang,DiaChiGiao,TinhTrang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ho", donDatHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "Ho", donDatHang.MaNhanVien);
            ViewBag.MaDonViVanChuyen = new SelectList(db.DonViVanChuyens, "MaDonVi", "TenDonVi", donDatHang.MaDonViVanChuyen);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangA/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Admin/DonDatHangA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
