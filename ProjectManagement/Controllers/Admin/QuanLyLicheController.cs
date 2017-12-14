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
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(db.DotKhoaLuans.ToList());
            }
        }

        // GET: QuanLyLiche/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
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

        public ActionResult DSD(decimal? DKId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (DKId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienKhoa = db.QuanLyLiches.Where(n => n.DotKhoaLuanId == DKId).ToList();
                if (sinhVienKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DKId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.DKLId = kh.DotKhoaLuanId;
                    ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                    ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", kh.DotKhoaLuanId);
                }
                else
                {
                    ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
                }
                
                return View(sinhVienKhoa.ToList());
            }
        }

        // GET: QuanLyLiche/Create
        public ActionResult Create(decimal DKId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (DKId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienThuocKhoa = db.QuanLyLiches.Where(n => n.DotKhoaLuanId == DKId).ToList();
                if (sinhVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DKId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                    ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                    ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", kh.DotKhoaLuanId);
                }
                else
                {
                    ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
                }
                //var list1 = db.SinhViens.ToList();
                //var list2 = db.SinhVienThuocKhoas.Where(p => p.KhoaId == KId).ToList();
                //var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                //ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
                return View();
            }
        }

        // POST: QuanLyLiche/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DotKhoaLuanId,QuanLyLichId,TieuDe,MocThoiGian,NoiDung")] QuanLyLich quanLyLich)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.QuanLyLiches.Max(u => u.QuanLyLichId);
                quanLyLich.QuanLyLichId = maxId + 1;
                var svKh = db.QuanLyLiches.Where(n => n.DotKhoaLuanId == quanLyLich.DotKhoaLuanId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == quanLyLich.DotKhoaLuanId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                    ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                }

                db.QuanLyLiches.Add(quanLyLich);
                db.SaveChanges();
                return RedirectToAction("DSD", new { DKId = quanLyLich.DotKhoaLuanId });
            }

            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", quanLyLich.DotKhoaLuanId);
            return View(quanLyLich);
        }

        // GET: QuanLyLiche/Edit/5
        public ActionResult Edit(decimal? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyLich quanLyLich = db.QuanLyLiches.Find(id);
            if (quanLyLich == null)
            {
                return HttpNotFound();
            }
            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", quanLyLich.DotKhoaLuanId);
            return View(quanLyLich);
        }

        // POST: QuanLyLiche/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DotKhoaLuanId,QuanLyLichId,TieuDe,MocThoiGian,NoiDung")] QuanLyLich quanLyLich)
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
        public ActionResult Delete(string id)
        {
            if (id == null)
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

        // POST: QuanLyLiche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuanLyLich quanLyLich = db.QuanLyLiches.Find(id);
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
