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
    
    public partial class GiangVienThuocKhoa
    {
        public decimal KhoaId { get; set; }
        public string GiangVienId { get; set; }
        public System.DateTime TuNgay { get; set; }
        public Nullable<System.DateTime> DenNgay { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
        public virtual Khoa Khoa { get; set; }
    }
}
