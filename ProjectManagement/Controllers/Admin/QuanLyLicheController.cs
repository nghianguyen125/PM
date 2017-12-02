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
    public class QuanLyLicheController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: QuanLyLiche
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var quanLyLiches = db.QuanLyLiches.Include(q => q.DotKhoaLuan);
                return View(quanLyLiches.OrderBy(n => n.MocThoiGian).ToList());
            }
                
        }

        // GET: QuanLyLiche/Details/5
        public ActionResult Details(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuanLyLich quanLyLich = db.QuanLyLiches.Find(id);
                if (quanLyLich == null)
                {
                    return HttpNotFound();
                }
                return View(quanLyLich);
            }
            
        }

        // GET: QuanLyLiche/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
                return View();
            }
                
        }

        // POST: QuanLyLiche/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DotKhoaLuanId,TieuDe,MocThoiGian,NoiDung")] QuanLyLich quanLyLich)
        {
            if (ModelState.IsValid)
            {
                db.QuanLyLiches.Add(quanLyLich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", quanLyLich.DotKhoaLuanId);
            return View(quanLyLich);
        }

        // GET: QuanLyLiche/Edit/5
        public ActionResult Edit(decimal? DId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (DId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuanLyLich quanLyLich = db.QuanLyLiches.Find(DId);
                if (quanLyLich == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", quanLyLich.DotKhoaLuanId);
                return View(quanLyLich);
            }
            
        }

        // POST: QuanLyLiche/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DotKhoaLuanId,TieuDe,MocThoiGian,NoiDung")] QuanLyLich quanLyLich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanLyLich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", quanLyLich.DotKhoaLuanId);
            return View(quanLyLich);
        }

        // GET: QuanLyLiche/Delete/5
        public ActionResult Delete(decimal? DId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (DId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                QuanLyLich quanLyLich = db.QuanLyLiches.Find(DId);
                if (quanLyLich == null)
                {
                    return HttpNotFound();
                }
                return View(quanLyLich);
            }
            
        }

        // POST: QuanLyLiche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? Did)
        {
            QuanLyLich quanLyLich = db.QuanLyLiches.Find(Did);
            db.QuanLyLiches.Remove(quanLyLich);
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
