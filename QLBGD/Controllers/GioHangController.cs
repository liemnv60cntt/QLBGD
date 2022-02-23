﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBGD.Models; 

namespace QLBGD.Controllers
{
    public class GioHangController : Controller
    {
        #region Giỏ hàng
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // Lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tạo list giỏ hàng (session[GioHang])
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }

            return listGioHang;
        }

        // Thêm giỏ hàng
        // định nghĩa tham số cho action để nhận tham số tương ứng
        public ActionResult ThemGioHang(string cMaSanPham, string strURL)
        {
            // tìm xem sản phẩm này đã có trong cửa hàng hay chưa
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.MaSanPham == cMaSanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy ra session giỏ hàng
            List<GioHang> listGioHang = LayGioHang();

            // Kiểm tra sách này đã tồn tại trong session[gioHang] hay chưa
            GioHang sanPham = listGioHang.Find(b => b.iMaSanPham == cMaSanPham);
            if (sanPham == null)
            {
                sanPham = new GioHang(cMaSanPham);

                // thêm sản phẩm mới vào giỏ hàng
                listGioHang.Add(sanPham);
                return Redirect(strURL);
            }
            else
            {
                sanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        // Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(string cMaSanPham, FormCollection f)
        {
            // kiểm tra mã sản phẩm có trong cơ dở dữ liệu ko?
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.MaSanPham == cMaSanPham);

            // Nếu get sai mã sản phẩm thì trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy giỏ hàng ra từ session
            List<GioHang> listGioHang = LayGioHang();

            // Kiểm tra san phảm có tồn tại trong session["GioHang"]
            GioHang sanPham = listGioHang.SingleOrDefault(b => b.iMaSanPham == cMaSanPham);

            // Nếu tồn tại thì cho sửa số lượng
            if (sanPham != null)
            {
                sanPham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("GioHang");
        }

        // Xóa giỏ hàng
        public ActionResult XoaGioHang(string cMaSanPham)
        {
            // kiểm tra mã sản phẩm có trong cơ dở dữ liệu ko?
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.MaSanPham == cMaSanPham);

            // Nếu get sai mã sản phẩm thì trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy giỏ hàng ra từ session
            List<GioHang> listGioHang = LayGioHang();

            // Kiểm tra san phảm có tồn tại trong session["GioHang"]
            GioHang sanPham = listGioHang.SingleOrDefault(b => b.iMaSanPham == cMaSanPham);

            // Nếu tồn tại thì cho sửa số lượng
            if (sanPham != null)
            {
                listGioHang.RemoveAll(a => a.iMaSanPham == cMaSanPham);
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return RedirectToAction("GioHang");
        }

        // Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null || TongSoLuong() == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            List<GioHang> listGioHang = LayGioHang();


            ViewBag.MaDonViVanChuyen = new SelectList(db.DonViVanChuyens, "MaDonVi", "TenDonVi");
            ViewBag.TongTien = TongSoTien();
            return View(listGioHang);
        }

        // xây dựng tính tổng số lượng và tổng tiền
        // tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }

            return iTongSoLuong;
        }

        // tính tổng số tiền
        private int TongSoTien()
        {
            int iTongSoTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang != null)
            {
                iTongSoTien = listGioHang.Sum(n => n.iThanhTien);
            }

            return iTongSoTien;
        }

        // tạo partial giỏ hàng 
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }

            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }

        // Xây dựng một view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (TongSoLuong() == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            List<GioHang> listGioHang = LayGioHang();

            ViewBag.TongTien = TongSoTien();
            return View(listGioHang);
        }
        #endregion



        #region Đặt hàng

        string LayMaDonHang()
        {
            var maMax = db.DonDatHangs.ToList().Select(n => n.MaDonDatHang).Max();
            int maDH = int.Parse(maMax.Substring(2)) + 1;
            string DH = String.Concat("00", maDH.ToString());
            return "DH" + DH.Substring(maDH.ToString().Length - 1);
        }

        // Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(string maDonViVanChuyen, string diaChiGiao = "")
        {
            // kiểm tra đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "KhachHang");
            }

            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            // Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();

            ddh.MaDonDatHang = LayMaDonHang();
            ddh.MaKhachHang = kh.MaKhachHang;
            ddh.MaNhanVien = "NV000";
            ddh.NgayDatHang = DateTime.Now;
            ddh.DiaChiGiao = kh.DiaChi;
            ddh.MaDonViVanChuyen = maDonViVanChuyen;

            if (diaChiGiao != "")
            {
                ddh.DiaChiGiao = diaChiGiao;
            }



            ddh.TinhTrang = 1;
            // Thêm chi tiết đơn hàng
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();

            foreach (var item in gh)
            {
                ChiTietDatHang ctDH = new ChiTietDatHang();
                ctDH.MaDonDatHang = ddh.MaDonDatHang;
                ctDH.MaSanPham = item.iMaSanPham;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.Gia = item.iThanhTien;
                db.ChiTietDatHangs.Add(ctDH);
            }

            gh.Clear();
            db.SaveChanges();
            return RedirectToAction("Index", "TrangChu");
        }

        #endregion
    }
}