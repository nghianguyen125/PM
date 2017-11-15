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
    public class SinhVienThuocKhoaController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienThuocKhoa
        public ActionResult Index()
        {
            var sinhVienThuocKhoas = db.SinhVienThuocKhoas.Include(s => s.Khoa).Include(s => s.SinhVien);
            return View(sinhVienThuocKhoas.ToList());
        }

        // GET: SinhVienThuocKhoa/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(id);
            if (sinhVienThuocKhoa == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienThuocKhoa);
        }

        // GET: SinhVienThuocKhoa/Create
        public ActionResult Create()
        {
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa");
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
            return View();
        }

        // POST: SinhVienThuocKhoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaId,SinhVienId,TuNgay,DenNgay")] SinhVienThuocKhoa sinhVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                DateTime ngay = sinhVienThuocKhoa.TuNgay;
                ngay = new DateTime(ngay.Year, ngay.Month, ngay.Day);
                sinhVienThuocKhoa.TuNgay = ngay;
                db.SinhVienThuocKhoas.Add(sinhVienThuocKhoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", sinhVienThuocKhoa.KhoaId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocKhoa.SinhVienId);
            return View(sinhVienThuocKhoa);
        }

        // GET: SinhVienThuocKhoa/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(id);
            if (sinhVienThuocKhoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", sinhVienThuocKhoa.KhoaId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocKhoa.SinhVienId);
            return View(sinhVienThuocKhoa);
        }

        // POST: SinhVienThuocKhoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaId,SinhVienId,TuNgay,DenNgay")] SinhVienThuocKhoa sinhVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienThuocKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", sinhVienThuocKhoa.KhoaId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocKhoa.SinhVienId);
            return View(sinhVienThuocKhoa);
        }

        // GET: SinhVienThuocKhoa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(id);
            if (sinhVienThuocKhoa == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienThuocKhoa);
        }

        // POST: SinhVienThuocKhoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(id);
            db.SinhVienThuocKhoas.Remove(sinhVienThuocKhoa);
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
