﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBGD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.DonDatHangs = new HashSet<DonDatHang>();
        }

        [Display(Name = "Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Chưa nhập họ")]
        public string Ho { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Chưa nhập tên")]
        public string Ten { get; set; }

        [Display(Name = "Giới tính")]
        public Nullable<bool> GioiTinh { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Chưa nhập số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Chưa nhập Email")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Chưa cập nhật trạng thái")]
        public string TrangThai { get; set; }

        [Display(Name = "Chức vụ")]
        public Nullable<bool> QuanLy { get; set; }

        [Display(Name = "Ảnh nhân viên")]
        public string AnhNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
