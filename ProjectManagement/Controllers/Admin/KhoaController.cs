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
    public class KhoaController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: Khoa
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                var khoa = from b in db.Khoas select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    khoa = db.Khoas.Where(s => s.TenKhoa.Contains(searchString));
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
                return View(khoa.OrderBy(s => s.TenKhoa).ToList().ToPagedList(pageNumber, pageSize));
            }
                
        }

        // GET: Khoa/Details/5
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
                Khoa khoa = db.Khoas.Find(id);
                if (khoa == null)
                {
                    return HttpNotFound();
                }
                return View(khoa);
            }

        }

        // GET: Khoa/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else return View();
        }

        // POST: Khoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaId,TenKhoa,DiaChi")] Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.Khoas.Max(u => u.KhoaId);
                khoa.KhoaId = maxId + 1;
                db.Khoas.Add(khoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khoa);
        }

        // GET: Khoa/Edit/5
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
                Khoa khoa = db.Khoas.Find(id);
                if (khoa == null)
                {
                    return HttpNotFound();
                }
                return View(khoa);
            }

        }

        // POST: Khoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaId,TenKhoa,DiaChi")] Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khoa);
        }

        // GET: Khoa/Delete/5
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
                Khoa khoa = db.Khoas.Find(id);
                if (khoa == null)
                {
                    return HttpNotFound();
                }
                return View(khoa);
            }

        }

        // POST: Khoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {

            var q = db.SinhVienThuocKhoas.Where(a => a.KhoaId == id);
            if (q.Any())
            {
                return RedirectToAction("KhongXoa", "User");
            }
            Khoa khoa = db.Khoas.Find(id);
            db.Khoas.Remove(khoa);
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
