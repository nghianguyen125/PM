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
    public class SinhVienNganhHocController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienNganhHoc
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else return View(db.Nganhs.ToList());
        }

        // GET: SinhVienNganhHoc/Details/5
        public ActionResult Details(string id)
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
                SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(id);
                if (sinhVienNganhHoc == null)
                {
                    return HttpNotFound();
                }
                return View(sinhVienNganhHoc);
            }
            
        }

        public ActionResult DSSV(decimal? NId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (NId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienNganhHoc = db.SinhVienNganhHocs.Where(n => n.NganhId == NId).ToList();
                if (sinhVienNganhHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Nganhs.Where(n => n.NganhId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.NganhId = kh.NganhId;
                    ViewBag.TenNganh = kh.TenNganh;
                }
                return View(sinhVienNganhHoc.ToList());
            }

        }

        // GET: SinhVienNganhHoc/Create
        public ActionResult Create(decimal? NId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (NId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienNganhHoc = db.SinhVienNganhHocs.Where(n => n.NganhId == NId).ToList();
                if (sinhVienNganhHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Nganhs.Where(n => n.NganhId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenNganh = kh.TenNganh;
                    ViewBag.IdNganh = kh.NganhId;
                    ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", kh.NganhId);
                }
                else
                {
                    ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
                }
                var list1 = db.SinhViens.ToList();
                var list2 = db.SinhVienNganhHocs.Where(p => p.NganhId == NId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                ViewBag.SinhVienId = new SelectList(sv, "SinhVienId", "HoTen");
                return View();
            }
            
        }

        // POST: SinhVienNganhHoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SinhVienId,NganhId,TuNgay,DenNgay,KhoaHocID")] SinhVienNganhHoc sinhVienNganhHoc)
        {
            if (ModelState.IsValid)
            {
                db.SinhVienNganhHocs.Add(sinhVienNganhHoc);
                db.SaveChanges();
                var svKh = db.SinhVienNganhHocs.Where(n => n.NganhId == sinhVienNganhHoc.NganhId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Nganhs.Where(n => n.NganhId == sinhVienNganhHoc.NganhId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.NganhId;
                    ViewBag.TenKhoaHoc = kh.TenNganh;
                }
                return RedirectToAction("DSSV", new { NId = sinhVienNganhHoc.NganhId });
            }

            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
            return View(sinhVienNganhHoc);
        }

        // GET: SinhVienNganhHoc/Edit/5
        public ActionResult Edit(decimal? NId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (NId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //var sinhVienNganhHoc= db.SinhVienNganhHocs.Where(n => n.NganhId == NId && n.SinhVienId == SVId).ToList();
                SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(NId, SVId);
                if (sinhVienNganhHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Nganhs.Where(n => n.NganhId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenNganh = kh.TenNganh;
                    ViewBag.IdNganh = kh.NganhId;
                    //ViewBag.NganhID = new SelectList(db.Nganhs, "NganhId", "TenNganh", kh.NganhId);
                }
                else
                {
                    ViewBag.NganhID = new SelectList(db.Nganhs, "NganhId", "TenNganh");
                }
                ViewBag.NganhID = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
                return View(sinhVienNganhHoc);
            }
           
        }

        // POST: SinhVienNganhHoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SinhVienId,NganhId,TuNgay,DenNgay,KhoaHocID")] SinhVienNganhHoc sinhVienNganhHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienNganhHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSSV", new { NId = sinhVienNganhHoc.NganhId });
            }
            ViewBag.NganhId = new SelectList(db.Nganhs, "NganhId", "TenNganh", sinhVienNganhHoc.NganhId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienNganhHoc.SinhVienId);
            return View(sinhVienNganhHoc);
        }

        // GET: SinhVienNganhHoc/Delete/5
        public ActionResult Delete(decimal? NId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (NId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(NId, SVId);
                if (sinhVienNganhHoc == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Nganhs.Where(n => n.NganhId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdNganhHoc = kh.NganhId;
                    ViewBag.TenNganhHoc = kh.TenNganh;
                }
                return View(sinhVienNganhHoc);
            }
            
        }

        // POST: SinhVienNganhHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? NId, string SVId)
        {
            SinhVienNganhHoc sinhVienNganhHoc = db.SinhVienNganhHocs.Find(NId, SVId);
            db.SinhVienNganhHocs.Remove(sinhVienNganhHoc);
            db.SaveChanges();
            return RedirectToAction("DSSV", new { NId = sinhVienNganhHoc.NganhId });
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
