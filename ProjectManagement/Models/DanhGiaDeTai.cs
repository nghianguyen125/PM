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

    public partial class DanhGiaDeTai
    {
        [Required]
        public decimal DeTaiId { get; set; }

        [Required]
        public string SinhVienId { get; set; }

        [Required]
        public string GiangVienId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Please enter valid number")]
        public decimal Diem { get; set; }

        [StringLength(500, ErrorMessage = "Maximum is 500")]
        public string NoiDungDanhGia { get; set; }
    
        public virtual DeTai DeTai { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}
