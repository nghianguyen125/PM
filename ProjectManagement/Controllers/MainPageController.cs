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
            else
            {
                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                //SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(id);
                //if (sinhVienKhoaHoc == null)
                //{
                //    return HttpNotFound();
                //}
                return View(db.QuanLyLiches.OrderBy(n => n.MocThoiGian).ToList());
            }
               
        }
    }
}