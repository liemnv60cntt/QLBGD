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
    public partial class LoaiSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiSanPham()
        {
            this.SanPhams = new HashSet<SanPham>();
        }

        [Display(Name = "Mã loại sản phẩm")]
        public string MaLoaiSanPham { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        [Required(ErrorMessage = "Chưa nhập tên loại sản phẩm")]
        public string TenLoaiSanPham { get; set; }

        [Display(Name = "Đơn vị tính")]
        [Required(ErrorMessage = "Chưa nhập đơn vị tính")]
        public string DVT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}