using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class LoaiTaiKhoanController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: LoaiTaiKhoan
        public ActionResult Index()
        {
            return View(db.LoaiTaiKhoans.ToList());
        }

        // GET: LoaiTaiKhoan/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // GET: LoaiTaiKhoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiTaiKhoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoaiTaiKhoanId,TenLoaiTaiKhoan")] LoaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTaiKhoans.Add(loaiTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiTaiKhoan);
        }

        // GET: LoaiTaiKhoan/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: LoaiTaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiTaiKhoanId,TenLoaiTaiKhoan")] LoaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiTaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiTaiKhoan);
        }

        // GET: LoaiTaiKhoan/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: LoaiTaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            db.LoaiTaiKhoans.Remove(loaiTaiKhoan);
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
