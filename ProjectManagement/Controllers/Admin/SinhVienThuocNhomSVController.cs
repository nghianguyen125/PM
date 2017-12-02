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
    public class SinhVienThuocNhomSVController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienThuocNhomSV
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.NhomSVs.ToList());
            }

        }

        // GET: SinhVienThuocNhomSV/Details/5
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
                SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(id);
                if (sinhVienThuocNhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(sinhVienThuocNhomSV);
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
                var sinhVienThuocNhom = db.SinhVienThuocNhomSVs.Where(n => n.NhomSVId == NId).ToList();
                if (sinhVienThuocNhom == null)
                {
                    return HttpNotFound();
                }
                var kh = db.NhomSVs.Where(n => n.NhomSVId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdNhom = kh.NhomSVId;
                    ViewBag.TenNhom = kh.TenNhom;
                }

                //var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.SinhVien).Where(n => (n.KhoaHocID == KHId && n.SinhVienId == SVId));
                return View(sinhVienThuocNhom.ToList());
            }
          
        }

        // GET: SinhVienThuocNhomSV/Create
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
                var sinhVienThuocNhom = db.SinhVienThuocNhomSVs.Where(n => n.NhomSVId == NId).ToList();
                if (sinhVienThuocNhom == null)
                {
                    return HttpNotFound();
                }
                var kh = db.NhomSVs.Where(n => n.NhomSVId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenNhomSV = kh.TenNhom;
                    ViewBag.IdNhomSV = kh.NhomSVId;
                    ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", kh.NhomSVId);
                }
                else
                {
                    ViewBag.NHomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom");
                }

                var list1 = db.SinhViens.ToList();
                var list2 = db.SinhVienThuocNhomSVs.Where(p => p.NhomSVId == NId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
                return View();
            }
           
        }

        // POST: SinhVienThuocNhomSV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhomSVId,SinhVienId,NgayPhan")] SinhVienThuocNhomSV sinhVienThuocNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.SinhVienThuocNhomSVs.Add(sinhVienThuocNhomSV);
                db.SaveChanges();

                var svKh = db.SinhVienThuocNhomSVs.Where(n => n.NhomSVId == sinhVienThuocNhomSV.NhomSVId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.NhomSVs.Where(n => n.NhomSVId == sinhVienThuocNhomSV.NhomSVId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdNhom = kh.NhomSVId;
                    ViewBag.TenNhom = kh.TenNhom;
                }
                return RedirectToAction("DSSV", new { NId = sinhVienThuocNhomSV.NhomSVId });
            }
            return View(sinhVienThuocNhomSV);
        }

        // GET: SinhVienThuocNhomSV/Edit/5
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
                SinhVienThuocNhomSV sinhVienThuocNhom = db.SinhVienThuocNhomSVs.Find(NId, SVId);
                if (sinhVienThuocNhom == null)
                {
                    return HttpNotFound();
                }
                var kh = db.NhomSVs.Where(n => n.NhomSVId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenNhomSV = kh.TenNhom;
                    ViewBag.IdNhom = kh.NhomSVId;
                    ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", kh.NhomSVId);
                }
                else
                {
                    ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom");
                }
                ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", sinhVienThuocNhom.NhomSVId);
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocNhom.SinhVienId);
                return View(sinhVienThuocNhom);
            }
          
        }

        // POST: SinhVienThuocNhomSV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhomSVId,SinhVienId,NgayPhan")] SinhVienThuocNhomSV sinhVienThuocNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienThuocNhomSV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSSV", new { NId = sinhVienThuocNhomSV.NhomSVId });
            }
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", sinhVienThuocNhomSV.NhomSVId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocNhomSV.SinhVienId);
            return View(sinhVienThuocNhomSV);
        }

        // GET: SinhVienThuocNhomSV/Delete/5
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
                SinhVienThuocNhomSV sinhVienThuocNhom = db.SinhVienThuocNhomSVs.Find(NId, SVId);
                if (sinhVienThuocNhom == null)
                {
                    return HttpNotFound();
                }
                var kh = db.NhomSVs.Where(n => n.NhomSVId == NId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdNhom = kh.NhomSVId;
                    ViewBag.TenNhom = kh.TenNhom;
                }
                return View(sinhVienThuocNhom);
            }
        }

        // POST: SinhVienThuocNhomSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? NId, string SVId)
        {
            SinhVienThuocNhomSV sinhVienThuocNhomSV = db.SinhVienThuocNhomSVs.Find(NId, SVId);
            db.SinhVienThuocNhomSVs.Remove(sinhVienThuocNhomSV);
            db.SaveChanges();
            return RedirectToAction("DSSV", new { NId = sinhVienThuocNhomSV.NhomSVId });
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
