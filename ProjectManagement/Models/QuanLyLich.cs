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
    
    public partial class QuanLyLich
    {
        public decimal DotKhoaLuanId { get; set; }
        public string QuanLyLichId { get; set; }
        public string TieuDe { get; set; }
        public System.DateTime MocThoiGian { get; set; }
        public string NoiDung { get; set; }
    
        public virtual DotKhoaLuan DotKhoaLuan { get; set; }
    }
}
