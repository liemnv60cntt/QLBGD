using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CryptoLib;
using QLBGD.Areas.NhanViens.Model;
using QLBGD.Models;

namespace QLBGD.Controllers
{
    public class KhachHangController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        // GET: KhachHang
        public ActionResult ThongTinKhachHang(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // chỉnh sửa thông tin khách hàng
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhachHang,Ho,Ten,GioiTinh,SDT,Email,MatKhau,TinhOrTP,DiaChi")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinKhachHang", new { @id = Session["MaKhachHang"] });
            }
            return View(khachHang);
        }

        // lấy mã khách hàng tự động
        string LayMaKhachHang()
        {
            var maMax = db.KhachHangs.ToList().Select(n => n.MaKhachHang).Max();
            int maKhachHang = int.Parse(maMax.Substring(2)) + 1;
            string NH = String.Concat("000", maKhachHang.ToString());
            return "KH" + NH.Substring(maKhachHang.ToString().Length - 1);
        }

        // lấy thơi gian
        DateTime LayThoiGian()
        {
            return DateTime.Now;
        }

        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model, NhatKy nhatKy)
        {
            var result = new TKKH().Login(model.email, Encryptor.MD5Hash(model.matKhau));

            string matKhau = Encryptor.MD5Hash(model.matKhau);

            KhachHang khachHang = db.KhachHangs.SingleOrDefault(kh => kh.Email == model.email && kh.MatKhau == matKhau);

            if (result && ModelState.IsValid)
            {
                //SessionHelper.setSession(new UserSession() { userName = model.userName });
                FormsAuthentication.SetAuthCookie(model.email, model.rememberMe);
                Session["Customer"] = model.email;

                // tạo một sesstion để lưu lại thông tin khách hàng
                Session["TaiKhoan"] = khachHang;

                // tạo một session để lấy mã khách hàng
                Session["MaKhachHang"] = khachHang.MaKhachHang;

                nhatKy.Username = Session["Customer"].ToString();
                nhatKy.TinhTrang = "Đăng nhập";
                nhatKy.ghiNho = LayThoiGian();
                db.NhatKies.Add(nhatKy);
                db.SaveChanges();

                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout(NhatKy nhatKy)
        {
            // ghi nhật ký đăng xuất
            nhatKy.Username = Session["Customer"].ToString();
            nhatKy.TinhTrang = "Đăng xuất";
            nhatKy.ghiNho = LayThoiGian();

            db.NhatKies.Add(nhatKy);
            db.SaveChanges();

            Session["TaiKhoan"] = null;
            Session["Customer"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "TrangChu");
        }

        [HttpGet]
        public ActionResult Regis()
        {
            ViewBag.MaKhachHang = LayMaKhachHang();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regis(KhachHang khachHang, NhatKy nhatky)
        {
            var result = new TKKH().Regis(khachHang.Email);

            ViewBag.MaKhachHang = LayMaKhachHang();
            if (result && ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email đã tồn tại");
            }
            else if (ModelState.IsValid)
            {
                khachHang.MaKhachHang = LayMaKhachHang();
                khachHang.MatKhau = Encryptor.MD5Hash(khachHang.MatKhau);
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();

                // ghi nhật ký đăng ký
                nhatky.Username = khachHang.Email;
                nhatky.TinhTrang = "Đăng ký";
                nhatky.ghiNho = LayThoiGian();

                db.NhatKies.Add(nhatky);
                db.SaveChanges();


                FormsAuthentication.SetAuthCookie(khachHang.Email, false);
                Session["Customer"] = khachHang.Email;

                // tạo một sesstion để lưu lại thông tin khách hàng
                Session["TaiKhoan"] = khachHang;

                // tạo một session để lấy mã khách hàng
                Session["MaKhachHang"] = khachHang.MaKhachHang;

                return RedirectToAction("Index", "TrangChu");
            }

            return View(khachHang);
        }
    }
}
