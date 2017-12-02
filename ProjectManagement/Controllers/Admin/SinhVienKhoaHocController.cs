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
    public class SinhVienKhoaHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienKhoaHoc
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                //var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.SinhVien);
                //return View(sinhVienKhoaHocs.ToList());
                var khoaHocs = db.KhoaHocs.Include(k => k.NamHoc).ToList();
                return View(khoaHocs);
            }
                
        }

        // GET: SinhVienKhoaHoc/Details/5
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
                SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(id);
                if (sinhVienKhoaHoc == null)
                {
                    return HttpNotFound();
                }
                return View(sinhVienKhoaHoc);
            }

        }

        public ActionResult DSSV(decimal? KHId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KHId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienKhoaHoc = db.SinhVienKhoaHocs.Where(n => n.KhoaHocID == KHId).ToList();
                if (sinhVienKhoaHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.KhoaHocs.Where(n => n.KhoaHocID == KHId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.KhoaHocID;
                    ViewBag.TenKhoaHoc = kh.TenKhoaHoc;
                }
                //var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.SinhVien).Where(n => (n.KhoaHocID == KHId && n.SinhVienId == SVId));
                return View(sinhVienKhoaHoc.ToList());
            }

        }

        // GET: SinhVienKhoaHoc/Create
        public ActionResult Create(decimal? KHId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KHId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienKhoaHoc = db.SinhVienKhoaHocs.Where(n => n.KhoaHocID == KHId).ToList();
                if (sinhVienKhoaHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.KhoaHocs.Where(n => n.KhoaHocID == KHId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenKhoaHoc = kh.TenKhoaHoc;
                    ViewBag.IdKhoaHoc = kh.KhoaHocID;
                    ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", kh.KhoaHocID);
                }

                else
                {
                    ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc");
                }

                var list1 = db.SinhViens.ToList();
                var list2 = db.SinhVienKhoaHocs.Where(p => p.KhoaHocID == KHId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                ViewBag.SinhVienId = new SelectList(sv, "SinhVienId", "HoTen");
                return View();
            }

        }

        // POST: SinhVienKhoaHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaHocID,SinhVienId,TuNgay,DenNgay")] SinhVienKhoaHoc sinhVienKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.SinhVienKhoaHocs.Add(sinhVienKhoaHoc);
                db.SaveChanges();

                var svKh = db.SinhVienKhoaHocs.Where(n => n.KhoaHocID == sinhVienKhoaHoc.KhoaHocID).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.KhoaHocs.Where(n => n.KhoaHocID == sinhVienKhoaHoc.KhoaHocID).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.KhoaHocID;
                    ViewBag.TenKhoaHoc = kh.TenKhoaHoc;
                }
                return RedirectToAction("DSSV", new { KHId = sinhVienKhoaHoc.KhoaHocID });
            }            
            return View(sinhVienKhoaHoc);
        }

        // GET: SinhVienKhoaHoc/Edit/5
        public ActionResult Edit(decimal? KHId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KHId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(KHId, SVId);
                if (sinhVienKhoaHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.KhoaHocs.Where(n => n.KhoaHocID == KHId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenKhoaHoc = kh.TenKhoaHoc;
                    ViewBag.IdKhoaHoc = kh.KhoaHocID;
                    ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", kh.KhoaHocID);
                }
                else
                {
                    ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc");
                }
                ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", sinhVienKhoaHoc.KhoaHocID);
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienKhoaHoc.SinhVienId);
                return View(sinhVienKhoaHoc);
            }

        }

        // POST: SinhVienKhoaHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaHocID,SinhVienId,TuNgay,DenNgay")] SinhVienKhoaHoc sinhVienKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienKhoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSSV", new { KHId = sinhVienKhoaHoc.KhoaHocID });
            }
            ViewBag.KhoaHocID = new SelectList(db.KhoaHocs, "KhoaHocID", "TenKhoaHoc", sinhVienKhoaHoc.KhoaHocID);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienKhoaHoc.SinhVienId);
            return View(sinhVienKhoaHoc);
        }

        // GET: SinhVienKhoaHoc/Delete/5
        public ActionResult Delete(decimal? KHId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KHId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(KHId, SVId);
                if (sinhVienKhoaHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.KhoaHocs.Where(n => n.KhoaHocID == KHId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.KhoaHocID;
                    ViewBag.TenKhoaHoc = kh.TenKhoaHoc;
                }
                return View(sinhVienKhoaHoc);
            }
           
        }

        // POST: SinhVienKhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? KHId, string SVId)
        {
            SinhVienKhoaHoc sinhVienKhoaHoc = db.SinhVienKhoaHocs.Find(KHId, SVId);
            db.SinhVienKhoaHocs.Remove(sinhVienKhoaHoc);
            db.SaveChanges();
            return RedirectToAction("DSSV", new { KHId = sinhVienKhoaHoc.KhoaHocID });
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
