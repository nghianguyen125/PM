//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class GiangVienThuocKhoa
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public decimal KhoaId { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string GiangVienId { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public System.DateTime TuNgay { get; set; }
        public Nullable<System.DateTime> DenNgay { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
        public virtual Khoa Khoa { get; set; }
    }
}
