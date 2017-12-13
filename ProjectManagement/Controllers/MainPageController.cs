using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagement.Models;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;

namespace ProjectManagement.Controllers.User
{
    public class MainPageController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();
        // GET: MainPage
        public ActionResult Index(string id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            
            var username = ProjectManagement.App_Start.Classes.UserManager.GetUserName;
            var tk = db.TaiKhoans.Where(z => z.Username == username).FirstOrDefault();
            if (tk.SinhVienId != null)
            {
                var sinhVienId = tk.SinhVienId;
                ViewBag.sinhvienId = sinhVienId;
            }
            if (tk.GiangVienId != null)
            {
                var giangVienId = tk.GiangVienId;
                ViewBag.giangvienId = giangVienId;
            }
            //var nhom = db.SinhVienThuocNhomSVs.Where(a => a.SinhVienId == tk.SinhVienId).FirstOrDefault();
            //ViewBag.nhom = nhom.NhomSV.TenNhom;

            //var sinhviens = db.SinhVienThuocNhomSVs.Where(b => b.NhomSVId == nhom.NhomSVId).ToList();

            //return View(sinhviens.ToList());
            return View(db.QuanLyLiches.OrderBy(n => n.MocThoiGian).ToList());

        }
    }
}