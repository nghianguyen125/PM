using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.User
{
    public class SinhVienThuocNhomSVsController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienThuocNhomSVs
        public ActionResult Index(string id)
        {
            var getid = ProjectManagement.App_Start.Classes.UserManager.GetUserId.ToString();
            var getuserid = db.SinhVienThuocNhomSVs.Where(n => n.SinhVienId == getid).FirstOrDefault();
            if (getuserid == null)
            {
                return HttpNotFound();
            }
            var sinhVienThuocNhomSVs = db.SinhVienThuocNhomSVs.Include(s => s.NhomSV).Include(s => s.SinhVien);
            var sv = db.SinhVienThuocNhomSVs.Include(s => s.NhomSV).Include(s => s.SinhVien).SingleOrDefault();
            if (sv != null)
            {
                ViewBag.id = sv.SinhVienId;
            }
            return View(sinhVienThuocNhomSVs.ToList());
        }

        // GET: SinhVienThuocNhomSVs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(id);
            if (sinhVienThuocNhomSV == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienThuocNhomSV);
        }

        // GET: SinhVienThuocNhomSVs/Create
        public ActionResult Create()
        {
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom");
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
            return View();
        }

        // POST: SinhVienThuocNhomSVs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhomSVId,SinhVienId,NgayPhan")] SinhVienThuocNhomSV sinhVienThuocNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.SinhVienThuocNhomSVs.Add(sinhVienThuocNhomSV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", sinhVienThuocNhomSV.NhomSVId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocNhomSV.SinhVienId);
            return View(sinhVienThuocNhomSV);
        }

        // GET: SinhVienThuocNhomSVs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(id);
            if (sinhVienThuocNhomSV == null)
            {
                return HttpNotFound();
            }
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", sinhVienThuocNhomSV.NhomSVId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocNhomSV.SinhVienId);
            return View(sinhVienThuocNhomSV);
        }

        // POST: SinhVienThuocNhomSVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhomSVId,SinhVienId,NgayPhan")] SinhVienThuocNhomSV sinhVienThuocNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienThuocNhomSV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", sinhVienThuocNhomSV.NhomSVId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocNhomSV.SinhVienId);
            return View(sinhVienThuocNhomSV);
        }

        // GET: SinhVienThuocNhomSVs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(id);
            if (sinhVienThuocNhomSV == null)
            {
                return HttpNotFound();
            }
            return View(sinhVienThuocNhomSV);
        }

        // POST: SinhVienThuocNhomSVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(id);
            db.SinhVienThuocNhomSVs.Remove(sinhVienThuocNhomSV);
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
