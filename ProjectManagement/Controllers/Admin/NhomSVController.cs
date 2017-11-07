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
    public class NhomSVController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: NhomSV
        public ActionResult Index()
        {
            return View(db.NhomSVs.ToList());
        }

        // GET: NhomSV/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomSV nhomSV = db.NhomSVs.Find(id);
            if (nhomSV == null)
            {
                return HttpNotFound();
            }
            return View(nhomSV);
        }

        // GET: NhomSV/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhomSV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhomSVId,TenNhom")] NhomSV nhomSV)
        {
            if (ModelState.IsValid)
            {
                db.NhomSVs.Add(nhomSV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhomSV);
        }

        // GET: NhomSV/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomSV nhomSV = db.NhomSVs.Find(id);
            if (nhomSV == null)
            {
                return HttpNotFound();
            }
            return View(nhomSV);
        }

        // POST: NhomSV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhomSVId,TenNhom")] NhomSV nhomSV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhomSV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhomSV);
        }

        // GET: NhomSV/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomSV nhomSV = db.NhomSVs.Find(id);
            if (nhomSV == null)
            {
                return HttpNotFound();
            }
            return View(nhomSV);
        }

        // POST: NhomSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            NhomSV nhomSV = db.NhomSVs.Find(id);
            db.NhomSVs.Remove(nhomSV);
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
