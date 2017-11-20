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
    
    public partial class SinhVien
    {
        public SinhVien()
        {
            this.DanhGiaDeTais = new HashSet<DanhGiaDeTai>();
            this.SinhVienKhoaHocs = new HashSet<SinhVienKhoaHoc>();
            this.SinhVienNganhHocs = new HashSet<SinhVienNganhHoc>();
            this.SinhVienThuocKhoas = new HashSet<SinhVienThuocKhoa>();
            this.SinhVienThuocNhomSVs = new HashSet<SinhVienThuocNhomSV>();
            this.TaiKhoans = new HashSet<TaiKhoan>();
        }
    
        public string SinhVienId { get; set; }
        public bool GioiTinh { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<int> SoTCHT { get; set; }
        public Nullable<int> SoTCCL { get; set; }
        public bool TrangThai { get; set; }
    
        public virtual ICollection<DanhGiaDeTai> DanhGiaDeTais { get; set; }
        public virtual ICollection<SinhVienKhoaHoc> SinhVienKhoaHocs { get; set; }
        public virtual ICollection<SinhVienNganhHoc> SinhVienNganhHocs { get; set; }
        public virtual ICollection<SinhVienThuocKhoa> SinhVienThuocKhoas { get; set; }
        public virtual ICollection<SinhVienThuocNhomSV> SinhVienThuocNhomSVs { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
