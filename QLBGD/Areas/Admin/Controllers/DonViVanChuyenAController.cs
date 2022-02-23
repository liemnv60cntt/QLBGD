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
    public class DonViVanChuyenAController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        //
        string LayMaDonVi()
        {
            var maMax = db.DonViVanChuyens.ToList().Select(n => n.MaDonVi).Max();
            int maDonVi = int.Parse(maMax.Substring(4)) + 1;
            string donVi = String.Concat("00", maDonVi.ToString());
            return "DVVC" + donVi.Substring(maDonVi.ToString().Length - 1);
        }

        // GET: Admin/DonViVanChuyenA
        public ActionResult Index()
        {
            return View(db.DonViVanChuyens.ToList());
        }

        // GET: Admin/DonViVanChuyenA/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViVanChuyen donViVanChuyen = db.DonViVanChuyens.Find(id);
            if (donViVanChuyen == null)
            {
                return HttpNotFound();
            }
            return View(donViVanChuyen);
        }

        // GET: Admin/DonViVanChuyenA/Create
        public ActionResult Create()
        {
            ViewBag.MaDonVi = LayMaDonVi();
            return View();
        }

        // POST: Admin/DonViVanChuyenA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonVi,TenDonVi,SDT,Email,AnhDVVC")] DonViVanChuyen donViVanChuyen)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgDVVC = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgDVVC.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/shippers/" + postedFileName); //
            imgDVVC.SaveAs(path); //

            if (ModelState.IsValid)
            {
                donViVanChuyen.MaDonVi = LayMaDonVi();
                donViVanChuyen.AnhDVVC = postedFileName;
                db.DonViVanChuyens.Add(donViVanChuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonVi = LayMaDonVi();
            return View(donViVanChuyen);
        }

        // GET: Admin/DonViVanChuyenA/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViVanChuyen donViVanChuyen = db.DonViVanChuyens.Find(id);
            if (donViVanChuyen == null)
            {
                return HttpNotFound();
            }
            return View(donViVanChuyen);
        }

        // POST: Admin/DonViVanChuyenA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonVi,TenDonVi,SDT,Email,AnhDVVC")] DonViVanChuyen donViVanChuyen)
        {
            var imgDVVC = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgDVVC.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/shippers/" + postedFileName);
                imgDVVC.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(donViVanChuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donViVanChuyen);
        }

        // GET: Admin/DonViVanChuyenA/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViVanChuyen donViVanChuyen = db.DonViVanChuyens.Find(id);
            if (donViVanChuyen == null)
            {
                return HttpNotFound();
            }
            return View(donViVanChuyen);
        }

        // POST: Admin/DonViVanChuyenA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonViVanChuyen donViVanChuyen = db.DonViVanChuyens.Find(id);
            db.DonViVanChuyens.Remove(donViVanChuyen);
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
