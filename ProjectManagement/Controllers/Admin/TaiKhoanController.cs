using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class TaiKhoanController : BaseController
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: /User/
        public ActionResult Index()
        {
            //if (!UserManager.Authenticated)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            var TaiKhoans = db.TaiKhoans.Include(u => u.LoaiTaiKhoan);
            return View(TaiKhoans.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(decimal? id)
        {
            if ((id + "").Trim() == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!ValidInput.validDecimal(id + ""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan user = db.TaiKhoans.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public ActionResult Create()
        {
            //if (!UserManager.Authenticated)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            ViewBag.LoaiTaiKhoanId = new SelectList(db.LoaiTaiKhoans, "LoaiTaiKhoanId", "LoaiTaiKhoanId");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public ActionResult Create([Bind(Include = "TaiKhoanId,Username,HoTen,Password,LoaiTaiKhoanId")] TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                bool valid = true;
                if (user.Username + "" == "")
                {
                    AddError("error", "Chưa Nhập Tên đăng nhập.");
                    valid = false;
                }
                if (user.Password + "" == "")
                {
                    AddError("error", "Chưa chọn Mật khẩu.");
                    valid = false;
                }
                if (user.HoTen + "" == "")
                {
                    AddError("error", "Chưa Nhập Họ tên.");
                    valid = false;
                }
                if (user.LoaiTaiKhoanId + "" == "")
                {
                    AddError("error", "Chưa chọn Vai trò.");
                    valid = false;
                }
                var checkExist = db.TaiKhoans.Where(u => u.Username == user.Username).ToList();
                if (checkExist.Any())
                {
                    AddError("error", "Tên Đăng nhập này đã tồn tại. Vui lòng chọn Tên Đăng nhập khác.");
                    valid = false;
                }
                if (valid)
                {
                    var maxId = db.TaiKhoans.Max(u => u.TaiKhoanId);
                    user.TaiKhoanId = maxId + 1;
                    string ps = Security.EncryptSha1(Security.EncryptMd5(user.Password).ToLower());
                    user.Password = ps;

                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.LoaiTaiKhoanId = new SelectList(db.LoaiTaiKhoans, "LoaiTaiKhoanId", "TenLoaiTaiKhoan", user.LoaiTaiKhoanId);
            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(decimal? id)
        {
            //if (!UserManager.Authenticated)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            if ((id + "").Trim() == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!ValidInput.validDecimal(id + ""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan user = db.TaiKhoans.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserCreatedBy = db.TaiKhoans.ToList();
            ViewBag.LoaiTaiKhoanId = new SelectList(db.LoaiTaiKhoans, "LoaiTaiKhoanId", "TenLoaiTaiKhoan", user.LoaiTaiKhoanId);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaiKhoanId,Username,HoTen,Password,LoaiTaiKhoanId")] TaiKhoan user)
        {
            ViewBag.AllowChangePass = (Request.Form["AllowChangePass"] == "on" ? true : false).ToString().ToLower();
            if (ModelState.IsValid)
            {
                bool valid = true;
                if (user.Username + "" == "")
                {
                    AddError("error", "Chưa Nhập Tên đăng nhập.");
                    valid = false;
                }
                if (user.Password + "" == "")
                {
                    AddError("error", "Chưa chọn Mật khẩu.");
                    valid = false;
                }
                if (user.HoTen + "" == "")
                {
                    AddError("error", "Chưa Nhập Họ tên.");
                    valid = false;
                }
                if (user.LoaiTaiKhoanId + "" == "")
                {
                    AddError("error", "Chưa chọn Vai trò.");
                    valid = false;
                }
                if (valid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    if (Request.Form["AllowChangePass"] == "on" ? true : false)
                    {
                        string ps = Security.EncryptSha1(Security.EncryptMd5(user.Password).ToLower());
                        user.Password = ps;
                        db.Entry(user).Property("Password").IsModified = true;
                    }
                    else
                    {
                        db.Entry(user).Property("Password").IsModified = false;
                    }
                    ;
                    db.SaveChanges();

                    ViewBag.UserCreatedBy = db.TaiKhoans.ToList();

                    return RedirectToAction("Index");
                }
            }
            ViewBag.LoaiTaiKhoanId = new SelectList(db.LoaiTaiKhoans, "LoaiTaiKhoanId", "TenLoaiTaiKhoan", user.LoaiTaiKhoanId);
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(decimal id)
        {
            //if (!UserManager.Authenticated)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan user = db.TaiKhoans.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TaiKhoan user = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}