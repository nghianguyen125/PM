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
    public class NamHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: NamHoc
        public ActionResult Index()
        {
            var namHocs = db.NamHocs.Include(n => n.NamHoc1);
            return View(namHocs.ToList());
        }

        // GET: NamHoc/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            return View(namHoc);
        }

        // GET: NamHoc/Create
        public ActionResult Create()
        {
            ViewBag.NamHocHocKyIdRoot = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy");
            return View();
        }

        // POST: NamHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NamHocHocKyId,TenNamHocHocKy,TuNgay,DenNgay,NamHocHocKyIdRoot")] NamHoc namHoc)
        {
            if (ModelState.IsValid)
            {
                db.NamHocs.Add(namHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NamHocHocKyIdRoot = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", namHoc.NamHocHocKyIdRoot);
            return View(namHoc);
        }

        // GET: NamHoc/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.NamHocHocKyIdRoot = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", namHoc.NamHocHocKyIdRoot);
            return View(namHoc);
        }

        // POST: NamHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NamHocHocKyId,TenNamHocHocKy,TuNgay,DenNgay,NamHocHocKyIdRoot")] NamHoc namHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(namHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NamHocHocKyIdRoot = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", namHoc.NamHocHocKyIdRoot);
            return View(namHoc);
        }

        // GET: NamHoc/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NamHoc namHoc = db.NamHocs.Find(id);
            if (namHoc == null)
            {
                return HttpNotFound();
            }
            return View(namHoc);
        }

        // POST: NamHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            NamHoc namHoc = db.NamHocs.Find(id);
            db.NamHocs.Remove(namHoc);
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
