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
    public partial class NhanHieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanHieu()
        {
            this.SanPhams = new HashSet<SanPham>();
        }

        [Display(Name = "Mã nhãn hiệu")]
        public string MaNhanHieu { get; set; }

        [Display(Name = "Tên nhãn hiệu")]
        [Required(ErrorMessage = "Chưa nhập mã nhãn hiệu")]
        public string TenNhanHieu { get; set; }

        [Display(Name = "Ghi chú")]
        [Required(ErrorMessage = "Chưa thêm ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Ảnh nhãn hiệu")]
        public string AnhNH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
