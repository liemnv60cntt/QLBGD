using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBGD.Models
{
    public class TKNV
    {
        private QuanLyBanGiayDep1Entities context = null;
        public TKNV()
        {
            context = new QuanLyBanGiayDep1Entities();
        }

        // có tồn tại tài khoản
        public bool Login(string email, string matKhau, bool quanLy)
        {
            object[] sqlParams =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", matKhau),
                new SqlParameter("@QuanLy", quanLy)
            };

            var result =
                context.Database.SqlQuery<bool>("Sp_QuanLy_Login @Email, @Password, @QuanLy", sqlParams).SingleOrDefault();

            return result;
        }
    }
}