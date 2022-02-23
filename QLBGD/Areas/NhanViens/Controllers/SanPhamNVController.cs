﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QLBGD.Models;

namespace QLBGD.Areas.NhanViens.Controllers
{
    public class SanPhamNVController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: NhanViens/SanPhamNV
        public ActionResult Index(string maSP = "", string tenSP = "", string maLSP = "", string maNH = "",
                                  string MauSac = "", string giaMin = "", string giaMax = "",
                                  string tinhTrang = "")
        {
            ViewBag.maSP = maSP;
            ViewBag.tenSP = tenSP;

            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.MaNH = new SelectList(db.NhanHieux, "MaNhanHieu", "TenNhanHieu");

            ViewBag.MauSac = MauSac;

            int min = 0, max = Int32.MaxValue;

            if (giaMin == "")
            {
                ViewBag.GiaMin = "";
                min = 0;
            }
            else
            {
                ViewBag.GiaMin = giaMin;
                min = int.Parse(giaMin);
            }
            if (giaMax == "")
            {
                max = Int32.MaxValue;
                ViewBag.GiaMax = "";
            }
            else
            {
                ViewBag.GiaMax = giaMax;
                max = int.Parse(giaMax);
            }

            ViewBag.TinhTrang = tinhTrang;

            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhanHieu)
                                      .Where(sp => sp.MaSanPham.Contains(maSP) &&
                                                    sp.TenSanPham.Contains(tenSP) &&
                                                    sp.MaLoaiSanPham.Contains(maLSP) &&
                                                    sp.MaNhanHieu.Contains(maNH) &&
                                                    sp.MauSac.Contains(MauSac) &&
                                                    sp.DonGia >= min && sp.DonGia <= max &&
                                                    sp.TinhTrang.Contains(tinhTrang));

            if (sanPhams.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(sanPhams.ToList());
        }
    }
}