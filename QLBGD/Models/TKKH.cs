using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLBGD.Models
{
    public class TKKH
    {
        private QuanLyBanGiayDep1Entities context = null;
        public TKKH()
        {
            context = new QuanLyBanGiayDep1Entities();
        }
        // có tồn tại tài khoản
        public bool Login(string email, string matKhau)
        {
            object[] sqlParams =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", matKhau)
            };

            var result =
                context.Database.SqlQuery<bool>("Sp_KhachHang_Login @Email, @Password", sqlParams).SingleOrDefault();

            return result;
        }

        public bool Regis(string email)
        {
            object[] sqlParams =
            {
                new SqlParameter("@Email", email),
            };

            var result =
                context.Database.SqlQuery<bool>("Sp_KhachHang_Regis @Email", sqlParams).SingleOrDefault();

            return result;
        }
    }
}