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
    public class PhanSinhVienChoDotKhoaLuanController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: PhanSinhVienChoDotKhoaLuan
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var phanSinhVienChoDotKhoaLuans = db.PhanSinhVienChoDotKhoaLuans.Include(p => p.DotKhoaLuan).Include(p => p.SinhVien);
                return View(phanSinhVienChoDotKhoaLuans.ToList());
            }
                
        }

        // GET: PhanSinhVienChoDotKhoaLuan/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanSinhVienChoDotKhoaLuan phanSinhVienChoDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Find(id);
            if (phanSinhVienChoDotKhoaLuan == null)
            {
                return HttpNotFound();
            }
            return View(phanSinhVienChoDotKhoaLuan);
        }

        public ActionResult DSSV(decimal? DId)
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
                var sinhVienDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).ToList();
                if (sinhVienDotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                var kl = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
                if (kl != null)
                {
                    ViewBag.IdDotKhoaLuan = kl.DotKhoaLuanId;
                    ViewBag.TenDotKhoaLuan = kl.TenDotKhoaLuan;
                }
                //var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.SinhVien).Where(n => (n.KhoaHocID == KHId && n.SinhVienId == SVId));
                return View(sinhVienDotKhoaLuan.ToList());
            }

        }

        // GET: PhanSinhVienChoDotKhoaLuan/Create
        public ActionResult Create(decimal? DId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                //ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
                //ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
                //return View();

                if (DId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).ToList();
                if (sinhVienDotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
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

                var list1 = db.SinhViens.ToList();
                var list2 = db.PhanSinhVienChoDotKhoaLuans.Where(p => p.DotKhoaLuanId == DId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                ViewBag.SinhVienId = new SelectList(sv, "SinhVienId", "HoTen");
                return View();
            }
                
        }

        // POST: PhanSinhVienChoDotKhoaLuan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DotKhoaLuanId,SinhVienId,NgayPhan")] PhanSinhVienChoDotKhoaLuan phanSinhVienChoDotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                db.PhanSinhVienChoDotKhoaLuans.Add(phanSinhVienChoDotKhoaLuan);
                db.SaveChanges();

                var svKh = db.PhanSinhVienChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == phanSinhVienChoDotKhoaLuan.DotKhoaLuanId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == phanSinhVienChoDotKhoaLuan.DotKhoaLuanId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                    ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                }
                return RedirectToAction("DSSV", new { DId = phanSinhVienChoDotKhoaLuan.DotKhoaLuanId });
            }
            return View(phanSinhVienChoDotKhoaLuan);
        }

        // GET: PhanSinhVienChoDotKhoaLuan/Edit/5
        public ActionResult Edit(decimal? DId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (DId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PhanSinhVienChoDotKhoaLuan sinhVienDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Find(DId, SVId);
                if (sinhVienDotKhoaLuan == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
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
                ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", sinhVienDotKhoaLuan.DotKhoaLuanId);
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienDotKhoaLuan.SinhVienId);
                return View(sinhVienDotKhoaLuan);
            }

        }

        // POST: PhanSinhVienChoDotKhoaLuan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DotKhoaLuanId,SinhVienId,NgayPhan")] PhanSinhVienChoDotKhoaLuan phanSinhVienChoDotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanSinhVienChoDotKhoaLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", phanSinhVienChoDotKhoaLuan.DotKhoaLuanId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", phanSinhVienChoDotKhoaLuan.SinhVienId);
            return View(phanSinhVienChoDotKhoaLuan);
        }

        // GET: PhanSinhVienChoDotKhoaLuan/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanSinhVienChoDotKhoaLuan phanSinhVienChoDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Find(id);
            if (phanSinhVienChoDotKhoaLuan == null)
            {
                return HttpNotFound();
            }
            return View(phanSinhVienChoDotKhoaLuan);
        }

        // POST: PhanSinhVienChoDotKhoaLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PhanSinhVienChoDotKhoaLuan phanSinhVienChoDotKhoaLuan = db.PhanSinhVienChoDotKhoaLuans.Find(id);
            db.PhanSinhVienChoDotKhoaLuans.Remove(phanSinhVienChoDotKhoaLuan);
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
