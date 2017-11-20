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
    public class SinhVienNganhHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienNganhHoc
        public ActionResult Index()
        {
            return View(db.SinhVienNganhHocs.ToList());
        }

        // GET: SinhVienNganhHoc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(id);
            if (sinhVienNganhHoc == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienNganhHoc);
        }

        // GET: SinhVienNganhHoc/Create
        public ActionResult Create()
        {
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh");
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
            return View();
        }

        // POST: SinhVienNganhHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SinhVienId,NganhId,TuNgay,DenNgay,KhoaHocID")] SinhVienNganhHoc sinhVienNganhHoc)
        {
            if (ModelState.IsValid)
            {
                db.SinhVienNganhHocs.Add(sinhVienNganhHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
            return View(sinhVienNganhHoc);
        }

        // GET: SinhVienNganhHoc/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(id);
            if (sinhVienNganhHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
            return View(sinhVienNganhHoc);
        }

        // POST: SinhVienNganhHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SinhVienId,NganhId,TuNgay,DenNgay,KhoaHocID")] SinhVienNganhHoc sinhVienNganhHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienNganhHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
            return View(sinhVienNganhHoc);
        }

        // GET: SinhVienNganhHoc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(id);
            if (sinhVienNganhHoc == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienNganhHoc);
        }

        // POST: SinhVienNganhHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(id);
            db.SinhVienNganhHocs.Remove(sinhVienNganhHoc);
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
