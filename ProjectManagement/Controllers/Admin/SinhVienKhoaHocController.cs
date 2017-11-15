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
    public class SinhVienKhoaHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienKhoaHoc
        public ActionResult Index()
        {
            var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.Nganh).Include(s => s.SinhVien);
            return View(sinhVienKhoaHocs.ToList());
        }

        // GET: SinhVienKhoaHoc/Details/5
        public ActionResult Details(decimal KHId = 0, string SVId = null, decimal NId = 0, DateTime? TuNg = null)
        {
            if (KHId == 0 || SVId == null || NId == 0 || TuNg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(KHId, SVId, NId, TuNg);
            if (sinhVienKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienKhoaHoc);
        }

        // GET: SinhVienKhoaHoc/Create
        public ActionResult Create()
        {
            ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc");
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh");
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
            return View();
        }

        // POST: SinhVienKhoaHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaHocID,SinhVienId,NganhId,TuNgay,DenNgay")] SinhVienKhoaHoc sinhVienKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                DateTime ngay = sinhVienKhoaHoc.TuNgay;
                ngay = new DateTime(ngay.Year, ngay.Month, ngay.Day);
                sinhVienKhoaHoc.TuNgay = ngay;
                db.SinhVienKhoaHocs.Add(sinhVienKhoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", sinhVienKhoaHoc.KhoaHocID);
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienKhoaHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienKhoaHoc.SinhVienId);
            return View(sinhVienKhoaHoc);
        }

        // GET: SinhVienKhoaHoc/Edit/5
        public ActionResult Edit(decimal KHId = 0, string SVId = null, decimal NId = 0, DateTime? TuNg = null)
        {
            if (KHId == 0 || SVId == null || NId == 0 || TuNg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(KHId, SVId, NId, TuNg);
            if (sinhVienKhoaHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", sinhVienKhoaHoc.KhoaHocID);
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienKhoaHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienKhoaHoc.SinhVienId);
            return View(sinhVienKhoaHoc);
        }

        // POST: SinhVienKhoaHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaHocID,SinhVienId,NganhId,TuNgay,DenNgay")] SinhVienKhoaHoc sinhVienKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienKhoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", sinhVienKhoaHoc.KhoaHocID);
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienKhoaHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienKhoaHoc.SinhVienId);
            return View(sinhVienKhoaHoc);
        }

        // GET: SinhVienKhoaHoc/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(id);
            if (sinhVienKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienKhoaHoc);
        }

        // POST: SinhVienKhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(id);
            db.SinhVienKhoaHocs.Remove(sinhVienKhoaHoc);
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
