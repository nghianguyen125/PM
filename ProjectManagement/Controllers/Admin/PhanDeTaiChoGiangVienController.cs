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
    public class PhanDeTaiChoGiangVienController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: PhanDeTaiChoGiangVien
        public ActionResult Index()
        {
            var phanDeTaiChoGiangViens = db.PhanDeTaiChoGiangViens.Include(p => p.DeTai).Include(p => p.GiangVien);
            return View(phanDeTaiChoGiangViens.ToList());
        }

        // GET: PhanDeTaiChoGiangVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoGiangVien phanDeTaiChoGiangVien = db.PhanDeTaiChoGiangViens.Find(id);
            if (phanDeTaiChoGiangVien == null)
            {
                return HttpNotFound();
            }
            return View(phanDeTaiChoGiangVien);
        }

        // GET: PhanDeTaiChoGiangVien/Create
        public ActionResult Create()
        {
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen");
            return View();
        }

        // POST: PhanDeTaiChoGiangVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GiangVienId,DeTaiId,NgayPhanDeTai")] PhanDeTaiChoGiangVien phanDeTaiChoGiangVien)
        {
            if (ModelState.IsValid)
            {
                db.PhanDeTaiChoGiangViens.Add(phanDeTaiChoGiangVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoGiangVien.DeTaiId);
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", phanDeTaiChoGiangVien.GiangVienId);
            return View(phanDeTaiChoGiangVien);
        }

        // GET: PhanDeTaiChoGiangVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoGiangVien phanDeTaiChoGiangVien = db.PhanDeTaiChoGiangViens.Find(id);
            if (phanDeTaiChoGiangVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoGiangVien.DeTaiId);
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", phanDeTaiChoGiangVien.GiangVienId);
            return View(phanDeTaiChoGiangVien);
        }

        // POST: PhanDeTaiChoGiangVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GiangVienId,DeTaiId,NgayPhanDeTai")] PhanDeTaiChoGiangVien phanDeTaiChoGiangVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanDeTaiChoGiangVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoGiangVien.DeTaiId);
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", phanDeTaiChoGiangVien.GiangVienId);
            return View(phanDeTaiChoGiangVien);
        }

        // GET: PhanDeTaiChoGiangVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoGiangVien phanDeTaiChoGiangVien = db.PhanDeTaiChoGiangViens.Find(id);
            if (phanDeTaiChoGiangVien == null)
            {
                return HttpNotFound();
            }
            return View(phanDeTaiChoGiangVien);
        }

        // POST: PhanDeTaiChoGiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhanDeTaiChoGiangVien phanDeTaiChoGiangVien = db.PhanDeTaiChoGiangViens.Find(id);
            db.PhanDeTaiChoGiangViens.Remove(phanDeTaiChoGiangVien);
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
