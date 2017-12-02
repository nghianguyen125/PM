using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    public class DanhGiaDeTaisController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: DanhGiaDeTais
        public ActionResult Index(string id)
        {
            var kh = db.DanhGiaDeTais.Where(p => p.GiangVienId == id).SingleOrDefault();
            if (kh != null)
            {
                ViewBag.IdGiangVien = kh.GiangVienId;
            }
            var danhGiaDeTais = db.DanhGiaDeTais.Include(d => d.DeTai).Include(d => d.SinhVien);
            return View(danhGiaDeTais.ToList());
        }

        // GET: DanhGiaDeTais/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(id);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Create
        public ActionResult Create()
        {
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
            return View();
        }

        // POST: DanhGiaDeTais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeTaiId,SinhVienId,GiangVienId,Diem,NoiDungDanhGia")] DanhGiaDeTai danhGiaDeTai)
        {
            if (ModelState.IsValid)
            {
                db.DanhGiaDeTais.Add(danhGiaDeTai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Edit/5
        public ActionResult Edit(decimal detaiid,string svid, string gvid)
        {
            if (svid == null || gvid == null || detaiid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(detaiid, svid, gvid);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // POST: DanhGiaDeTais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeTaiId,SinhVienId,GiangVienId,Diem,NoiDungDanhGia")] DanhGiaDeTai danhGiaDeTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhGiaDeTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Delete/5
        public ActionResult Delete(decimal detaiid, string svid, string gvid)
        {
            if (svid == null || gvid == null || detaiid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(detaiid, svid, gvid);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            return View(danhGiaDeTai);
        }

        // POST: DanhGiaDeTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(id);
            db.DanhGiaDeTais.Remove(danhGiaDeTai);
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
