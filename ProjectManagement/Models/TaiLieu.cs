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
    
    public partial class TaiLieu
    {
        public decimal TaiLieuId { get; set; }
        public string TenTaiLieu { get; set; }
        public string TepTinDinhKem { get; set; }
        public Nullable<decimal> DeTaiId { get; set; }
    
        public virtual DeTai DeTai { get; set; }
    }
}
