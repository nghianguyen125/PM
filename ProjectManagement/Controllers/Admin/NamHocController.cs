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
    public class NamHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: NamHoc
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                var namHoc = from b in db.NamHocs select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    namHoc = db.NamHocs.Where(s => s.TenNamHocHocKy.Contains(searchString));
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
                return View(namHoc.OrderBy(s => s.TenNamHocHocKy).ToList().ToPagedList(pageNumber, pageSize));
            }

        }

        // GET: NamHoc/Details/5
        public ActionResult Details(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
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
          
        }

        // GET: NamHoc/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.NamHocHocKyIdRoot = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy");
                return View();
            }
                
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
                var maxId = db.NamHocs.Max(u => u.NamHocHocKyId);
                namHoc.NamHocHocKyId = maxId + 1;
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
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
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
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
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
            
        }

        // POST: NamHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            var q = db.KhoaHocs.Where(a => a.NamHocHocKyId == id);
            var p = db.DotKhoaLuans.Where(b => b.NamHocHocKyId == id);
            if (q.Any() || p.Any())
            {
                return RedirectToAction("KhongXoa", "User");
            }
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
