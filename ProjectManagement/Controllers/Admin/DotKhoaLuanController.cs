using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class DotKhoaLuanController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: DotKhoaLuan
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dotKhoaLuans = db.DotKhoaLuans.Include(d => d.NamHoc);
                return View(dotKhoaLuans.ToList());
            }
        }

        // GET: DotKhoaLuan/Details/5
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
                DotKhoaLuan dotKhoaLuan = db.DotKhoaLuans.Find(id);
                if (dotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                return View(dotKhoaLuan);
            }
            
        }

        // GET: DotKhoaLuan/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy");
                ViewBag.DotKhoaLuanId = new SelectList(db.QuanLyLiches, "DotKhoaLuanId", "TieuDe");
                return View();
            }
        }

        // POST: DotKhoaLuan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DotKhoaLuanId,TenDotKhoaLuan,NamHocHocKyId")] DotKhoaLuan dotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.DotKhoaLuans.Max(u => u.DotKhoaLuanId);
                dotKhoaLuan.DotKhoaLuanId = maxId + 1;
                db.DotKhoaLuans.Add(dotKhoaLuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", dotKhoaLuan.NamHocHocKyId);
            ViewBag.DotKhoaLuanId = new SelectList(db.QuanLyLiches, "DotKhoaLuanId", "TieuDe", dotKhoaLuan.DotKhoaLuanId);
            return View(dotKhoaLuan);
        }

        // GET: DotKhoaLuan/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DotKhoaLuan dotKhoaLuan = db.DotKhoaLuans.Find(id);
                if (dotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", dotKhoaLuan.NamHocHocKyId);
                ViewBag.DotKhoaLuanId = new SelectList(db.QuanLyLiches, "DotKhoaLuanId", "TieuDe", dotKhoaLuan.DotKhoaLuanId);
                return View(dotKhoaLuan);
            }
            
        }

        // POST: DotKhoaLuan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DotKhoaLuanId,TenDotKhoaLuan,NamHocHocKyId")] DotKhoaLuan dotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotKhoaLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NamHocHocKyId = new SelectList(db.NamHocs, "NamHocHocKyId", "TenNamHocHocKy", dotKhoaLuan.NamHocHocKyId);
            ViewBag.DotKhoaLuanId = new SelectList(db.QuanLyLiches, "DotKhoaLuanId", "TieuDe", dotKhoaLuan.DotKhoaLuanId);
            return View(dotKhoaLuan);
        }

        // GET: DotKhoaLuan/Delete/5
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
                DotKhoaLuan dotKhoaLuan = db.DotKhoaLuans.Find(id);
                if (dotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                return View(dotKhoaLuan);
            }
           
        }

        // POST: DotKhoaLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            var q = db.PhanDeTaiChoDotKhoaLuans.Where(a => a.DotKhoaLuanId == id);
            var p = db.QuanLyLiches.Where(b => b.DotKhoaLuanId == id);
            if (q.Any() || p.Any())
            {
                return RedirectToAction("KhongXoa", "User");
            }
            DotKhoaLuan dotKhoaLuan = db.DotKhoaLuans.Find(id);
            db.DotKhoaLuans.Remove(dotKhoaLuan);
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
