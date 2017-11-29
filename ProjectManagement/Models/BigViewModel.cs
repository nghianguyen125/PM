using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class BigViewModel
    {
        public IEnumerable<TaiKhoan> TaiKhoan { get; set; }
        public IEnumerable<SinhVienThuocNhomSV> SVTNSV { get; set; }
    }   
}