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
    
    public partial class NamHoc
    {
        public NamHoc()
        {
            this.KhoaHocs = new HashSet<KhoaHoc>();
            this.NamHocs1 = new HashSet<NamHoc>();
        }
    
        public decimal NamHocHocKyId { get; set; }
        public string ChiTiet { get; set; }
        public Nullable<System.DateTime> TuNgay { get; set; }
        public Nullable<System.DateTime> DenNgay { get; set; }
        public Nullable<decimal> NamHocHocKyIdRoot { get; set; }
    
        public virtual ICollection<KhoaHoc> KhoaHocs { get; set; }
        public virtual ICollection<NamHoc> NamHocs1 { get; set; }
        public virtual NamHoc NamHoc1 { get; set; }
    }
}
