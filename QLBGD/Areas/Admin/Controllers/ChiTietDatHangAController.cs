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
    public class ChiTietDatHangAController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: Admin/ChiTietDatHangA
        public ActionResult Index()
        {
            var chiTietDatHangs = db.ChiTietDatHangs.Include(c => c.DonDatHang).Include(c => c.SanPham);
            return View(chiTietDatHangs.ToList());
        }

        // GET: Admin/ChiTietDatHangA/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHangA/Create
        public ActionResult Create()
        {
            ViewBag.MaDonDatHang = new SelectList(db.DonDatHangs, "MaDonDatHang", "MaKhachHang");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/ChiTietDatHangA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonDatHang,MaSanPham,SoLuong,Gia")] ChiTietDatHang chiTietDatHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDatHangs.Add(chiTietDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonDatHang = new SelectList(db.DonDatHangs, "MaDonDatHang", "MaKhachHang", chiTietDatHang.MaDonDatHang);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDatHang.MaSanPham);
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHangA/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonDatHang = new SelectList(db.DonDatHangs, "MaDonDatHang", "MaKhachHang", chiTietDatHang.MaDonDatHang);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDatHang.MaSanPham);
            return View(chiTietDatHang);
        }

        // POST: Admin/ChiTietDatHangA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonDatHang,MaSanPham,SoLuong,Gia")] ChiTietDatHang chiTietDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonDatHang = new SelectList(db.DonDatHangs, "MaDonDatHang", "MaKhachHang", chiTietDatHang.MaDonDatHang);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDatHang.MaSanPham);
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHangA/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDatHang);
        }

        // POST: Admin/ChiTietDatHangA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            db.ChiTietDatHangs.Remove(chiTietDatHang);
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
