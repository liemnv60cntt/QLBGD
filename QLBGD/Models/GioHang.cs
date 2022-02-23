using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBGD.Models
{
    public class GioHang
    {
        QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        public string iMaSanPham { get; set; }
        public string iAnhSanPham { get; set; }
        public string iTenSanPham { get; set; }
        public int iDonGia { get; set; }
        public int iSoLuong { get; set; }

        public int iThanhTien
        {
            get { return iDonGia * iSoLuong; }
        }

        // hàm tạo cho giỏ hàng
        public GioHang(string MaSanPham)
        {
            iMaSanPham = MaSanPham;
            SanPham sanPham = db.SanPhams.Single(sp => sp.MaSanPham == iMaSanPham);
            iAnhSanPham = sanPham.AnhSP;
            iTenSanPham = sanPham.TenSanPham;
            iDonGia = sanPham.DonGia;
            iSoLuong = 1;
        }
    }
}