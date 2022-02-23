using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBGD.Areas.Admin.Model
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản!")]
        public string username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string password { get; set; }
        public bool quanly { get; set; }
        public bool rememberMe { get; set; }
    }
}