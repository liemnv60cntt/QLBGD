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
    public class NhanHieuAController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // lấy mã nhãn hiệu
        string LayMaNH()
        {
            var maMax = db.NhanHieux.ToList().Select(n => n.MaNhanHieu).Max();
            int maNH = int.Parse(maMax.Substring(2)) + 1;
            string NH = String.Concat("00", maNH.ToString());
            return "NH" + NH.Substring(maNH.ToString().Length - 1);
        }

        // GET: Admin/NhanHieuA
        public ActionResult Index()
        {
            return View(db.NhanHieux.ToList());
        }

        // GET: Admin/NhanHieuA/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanHieu nhanHieu = db.NhanHieux.Find(id);
            if (nhanHieu == null)
            {
                return HttpNotFound();
            }
            return View(nhanHieu);
        }

        // GET: Admin/NhanHieuA/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaNH = LayMaNH();
            return View();
        }

        // POST: Admin/NhanHieuA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhanHieu,TenNhanHieu,GhiChu,AnhNH")] NhanHieu nhanHieu)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNH = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNH.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/brands/" + postedFileName); //
            imgNH.SaveAs(path); //

            if (ModelState.IsValid)
            {
                nhanHieu.MaNhanHieu = LayMaNH();
                nhanHieu.AnhNH = postedFileName;
                db.NhanHieux.Add(nhanHieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNH = LayMaNH();
            return View(nhanHieu);
        }

        // GET: Admin/NhanHieuA/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanHieu nhanHieu = db.NhanHieux.Find(id);
            if (nhanHieu == null)
            {
                return HttpNotFound();
            }
            return View(nhanHieu);
        }

        // POST: Admin/NhanHieuA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhanHieu,TenNhanHieu,GhiChu,AnhNH")] NhanHieu nhanHieu)
        {
            var imgNH = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNH.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/brands/" + postedFileName);
                imgNH.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(nhanHieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanHieu);
        }

        // GET: Admin/NhanHieuA/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanHieu nhanHieu = db.NhanHieux.Find(id);
            if (nhanHieu == null)
            {
                return HttpNotFound();
            }
            return View(nhanHieu);
        }

        // POST: Admin/NhanHieuA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanHieu nhanHieu = db.NhanHieux.Find(id);
            db.NhanHieux.Remove(nhanHieu);
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
