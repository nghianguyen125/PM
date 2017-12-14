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
    public class KhoaHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: KhoaHoc
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                var khoahoc = from b in db.KhoaHocs select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    khoahoc = db.KhoaHocs.Where(s => s.TenKhoaHoc.Contains(searchString));
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
                return View(khoahoc.OrderBy(s => s.TenKhoaHoc).ToList().ToPagedList(pageNumber, pageSize));
            }
                
        }

        // GET: KhoaHoc/Details/5
        public ActionResult Details(decimal? id)
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
                KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
                if (khoaHoc == null)
                {
                    return HttpNotFound();
                }
                return View(khoaHoc);
            }

        }

        // GET: KhoaHoc/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy");
                return View();
            }
 
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
                var maxId = db.KhoaHocs.Max(u => u.KhoaHocID);
                khoaHoc.KhoaHocID = maxId + 1;
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
                KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
                if (khoaHoc == null)
                {
                    return HttpNotFound();
                }
                ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", khoaHoc.NamHocHocKyId);
                return View(khoaHoc);
            }

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
                KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
                if (khoaHoc == null)
                {
                    return HttpNotFound();
                }
                return View(khoaHoc);
            }

        }

        // POST: KhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            var q = db.SinhVienKhoaHocs.Where(a => a.KhoaHocID == id);
            if (q.Any())
            {
                return RedirectToAction("KhongXoa", "User");
            }
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
