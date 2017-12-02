using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class NganhController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: Nganh
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                var nganh = from b in db.Nganhs select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    nganh = db.Nganhs.Where(s => s.TenNganh.Contains(searchString));
                }
                ViewBag.SearchString = searchString;
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(nganh.OrderBy(s => s.TenNganh).ToList().ToPagedList(pageNumber, pageSize));
            }
               
        }

        // GET: Nganh/Details/5
        public ActionResult Details(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nganh nganh = db.Nganhs.Find(id);
                if (nganh == null)
                {
                    return HttpNotFound();
                }
                return View(nganh);
            }
           
        }

        // GET: Nganh/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa");
                return View();
            }
               
        }

        // POST: Nganh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NganhId,TenNganh,KhoaId")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Nganhs.Add(nganh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nganh);
        }

        // GET: Nganh/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nganh nganh = db.Nganhs.Find(id);
                if (nganh == null)
                {
                    return HttpNotFound();
                }
                ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", nganh.KhoaId);
                return View(nganh);

            }
           
        }

        // POST: Nganh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NganhId,TenNganh,KhoaId")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", nganh.KhoaId);
            return View(nganh);
        }

        // GET: Nganh/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nganh nganh = db.Nganhs.Find(id);
                if (nganh == null)
                {
                    return HttpNotFound();
                }
                return View(nganh);
            }
          
        }

        // POST: Nganh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Nganh nganh = db.Nganhs.Find(id);
            db.Nganhs.Remove(nganh);
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
