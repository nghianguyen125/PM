using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;

namespace ProjectManagement.Controllers
{
    public class DeTaisController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: DeTais
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var username = ProjectManagement.App_Start.Classes.UserManager.GetUserName;
                var tk = db.TaiKhoans.Where(z => z.Username == username).FirstOrDefault();

                var nhom = db.SinhVienThuocNhomSVs.Where(a => a.SinhVienId == tk.SinhVienId).SingleOrDefault();
                ViewBag.nhom = nhom.NhomSV.TenNhom;

                var sinhVienId = tk.SinhVienId;
                ViewBag.sinhvienId = sinhVienId;

                var nhomSV = db.NhomSVs.Where(z => z.NhomSVId == nhom.NhomSVId).SingleOrDefault();
                var deTai = db.PhanDeTaiChoNhomSVs.Where(q => q.NhomSVId == nhomSV.NhomSVId).ToList();

                //var sinhVienThuocNhom = db.SinhVienThuocNhomSVs.Where(a => a.NhomSVId == nhomSVId.NhomSVId).ToList();
                return View(deTai.ToList());
                //return View(db.DeTais.ToList());
            }
        }




        // GET: DeTais/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            return View(deTai);
        }

        // GET: DeTais/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: DeTais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeTaiId,TenDeTai,MoTa,SoLuongThanhVien,NgayTao,NgayDangKy")] DeTai deTai)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.DeTais.Max(u => u.DeTaiId);
                deTai.DeTaiId = maxId + 1;
                db.DeTais.Add(deTai);
                db.SaveChanges();
                return RedirectToAction("Index", "MainPage");
            }

            return View(deTai);
        }

        // GET: DeTais/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            return View(deTai);
        }

        // POST: DeTais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeTaiId,TenDeTai,MoTa,SoLuongThanhVien,NgayTao,NgayDangKy")] DeTai deTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deTai);
        }

        // GET: DeTais/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            return View(deTai);
        }

        // POST: DeTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DeTai deTai = db.DeTais.Find(id);
            db.DeTais.Remove(deTai);
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