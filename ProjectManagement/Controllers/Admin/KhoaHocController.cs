using System
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
    public class KhoaHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: KhoaHoc
        public ActionResult Index()
        {
            var khoaHocs = db.KhoaHocs.Include(k => k.NamHoc);
            return View(khoaHocs.ToList());
        }

        // GET: KhoaHoc/Details/5
        public ActionResult Details(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // GET: KhoaHoc/Create
        public ActionResult Create()
        {
            ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy");
            return View();
        }

        // POST: KhoaHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaHocID,TenKhoaHoc,NamHocHocKyId")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.KhoaHocs.Add(khoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NamHocHocKyId = new SelectList(db.KhoaHocs, "NamHocHocKyId", "TenKhoaHoc", khoaHoc.NamHocHocKyId);
            return View(khoaHoc);
        }

        // GET: KhoaHoc/Edit/5
        public ActionResult Edit(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", khoaHoc.NamHocHocKyId);
            return View(khoaHoc);
        }

        // POST: KhoaHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaHocID,TenKhoaHoc,NamHocHocKyId")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenKhoaHoc", khoaHoc.NamHocHocKyId);
            return View(khoaHoc);
        }

        // GET: KhoaHoc/Delete/5
        public ActionResult Delete(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // POST: KhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            db.KhoaHocs.Remove(khoaHoc);
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
Implementing Basic CRUD Functionality with the Entity Framework in ASP.NET MVC Application
The Contoso University sample web application demonstrates how to create ASP.NET MVC 5 applications using the Entity Framework 6 Code First and Visual Studio...
docs.microsoft.com
