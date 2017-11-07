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
    public class TinTucController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: TinTuc
        public ActionResult Index()
        {
            return View(db.TinTucThongBaos.ToList());
        }

        // GET: TinTuc/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTuc = db.TinTucThongBaos.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: TinTuc/Create
        public ActionResult Create()
        {
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username");
            return View();
        }

        // POST: TinTuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TinTucId,TieuDe,NoiDung,NgayDang,TaiKhoanId")] TinTucThongBao tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucThongBaos.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username");
            return View(tinTuc);
        }

        // GET: TinTuc/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTuc = db.TinTucThongBaos.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username");
            return View(tinTuc);
        }

        // POST: TinTuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TinTucId,TieuDe,NoiDung,NgayDang,TaiKhoanId")] TinTucThongBao tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username");
            return View(tinTuc);
        }

        // GET: TinTuc/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTuc = db.TinTucThongBaos.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: TinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TinTucThongBao tinTuc = db.TinTucThongBaos.Find(id);
            db.TinTucThongBaos.Remove(tinTuc);
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
