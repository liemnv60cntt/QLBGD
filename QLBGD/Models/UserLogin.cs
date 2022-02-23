using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBGD.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string matKhau { get; set; }

        public bool rememberMe { get; set; }
    }
}