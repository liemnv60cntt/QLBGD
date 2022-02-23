using CryptoLib;
using QLBGD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QLBGD.Areas.Admin.Model;
namespace QLBGD.Areas.Admin.Controllers
{
    public class LoginAController : Controller
    {
        private QuanLyBanGiayDep1Entities db = new QuanLyBanGiayDep1Entities();

        DateTime LayThoiGian()
        {
            return DateTime.Now;
        }

        // GET: Admin/LoginA
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Admin = 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AdminLogin model, NhatKy nhatKy)
        {

            var result = new TKNV().Login(model.username, Encryptor.MD5Hash(model.password), model.quanly);

            Session["Staff"] = model.username;
            Session["QuanLy"] = model.quanly;

            if (result && ModelState.IsValid && model.quanly == true) // check xem đây có phải là tài khoản của quản lý hay không
            {
                FormsAuthentication.SetAuthCookie(model.username, model.rememberMe);

                // Lưu nhật ký đăng nhập của quản lý
                nhatKy.Username = model.username;
                nhatKy.TinhTrang = "Đăng nhập - Quản lý";
                nhatKy.ghiNho = LayThoiGian();
                db.NhatKies.Add(nhatKy);
                db.SaveChanges();

                return RedirectToAction("Index", "AdminHome");
            }
            else if (result && ModelState.IsValid && model.quanly == false) // check xem đây có phải là tài khoản của nhân viên hay không
            {
                FormsAuthentication.SetAuthCookie(model.username, model.rememberMe);

                // Lưu nhật ký đăng xuất của nhân viên
                nhatKy.Username = model.username;
                nhatKy.TinhTrang = "Đăng nhập - Nhân viên";
                nhatKy.ghiNho = LayThoiGian();
                db.NhatKies.Add(nhatKy);
                db.SaveChanges();

                return RedirectToAction("Index", "NhanVienHome", new { area = "NhanViens" });
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
            }

            return View(model);
        }

        public ActionResult Logout(NhatKy nhatKy)
        {
            FormsAuthentication.SignOut();

            nhatKy.Username = Session["Staff"].ToString();
            nhatKy.TinhTrang = "Đăng xuất";
            nhatKy.ghiNho = LayThoiGian();
            db.NhatKies.Add(nhatKy);
            db.SaveChanges();

            Session["Staff"] = null;
            Session["QuanLy"] = null;

            return RedirectToAction("Index", "LoginA");
        }
    }
}