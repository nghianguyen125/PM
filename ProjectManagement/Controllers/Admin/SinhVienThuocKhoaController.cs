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
    public class SinhVienThuocKhoaController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: SinhVienThuocKhoa
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.Khoas.ToList());
            }
              
        }

        // GET: SinhVienThuocKhoa/Details/5
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
                SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(id);
                if (sinhVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(sinhVienThuocKhoa);
            }
          
        }

        public ActionResult DSSV(decimal KId = 0)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienKhoa = db.SinhVienThuocKhoas.Where(n => n.KhoaId == KId).ToList();
                if (sinhVienKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == KId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.KhoaId = kh.KhoaId;
                    ViewBag.TenKhoa = kh.TenKhoa;
                }
                return View(sinhVienKhoa.ToList());
            }
           
        }


        // GET: SinhVienThuocKhoa/Create
        public ActionResult Create(decimal? KId = 0)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var sinhVienThuocKhoa = db.SinhVienThuocKhoas.Where(n => n.KhoaId == KId).ToList();
                if (sinhVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == KId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenKhoa = kh.TenKhoa;
                    ViewBag.IdKhoa = kh.KhoaId;
                    ViewBag.KhoaID = new SelectList(db.Khoas, "KhoaId", "TenKhoa", kh.KhoaId);
                }
                else
                {
                    ViewBag.KhoaID = new SelectList(db.Khoas, "KhoaId", "TenKhoa");
                }
                var list1 = db.SinhViens.ToList();
                var list2 = db.SinhVienThuocKhoas.Where(p => p.KhoaId == KId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.SinhVienId == p.SinhVienId)).ToList();

                ViewBag.SinhVienId = new SelectList(sv, "SinhVienId", "HoTen");
                return View();
            }
            
        }

        // POST: SinhVienThuocKhoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaId,SinhVienId,TuNgay,DenNgay")] SinhVienThuocKhoa sinhVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                //if (db.SinhVienThuocKhoas.Any(p => p.SinhVienId == sinhVienThuocKhoa.SinhVienId))
                //{
                //    ModelState.AddModelError("TenSinhVien", "Sinh Vien Da Ton Tai.");
                //}
                
                var svKh = db.SinhVienThuocKhoas.Where(n => n.KhoaId == sinhVienThuocKhoa.KhoaId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == sinhVienThuocKhoa.KhoaId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.KhoaId;
                    ViewBag.TenKhoaHoc = kh.TenKhoa;
                }

                db.SinhVienThuocKhoas.Add(sinhVienThuocKhoa);
                db.SaveChanges();
                return RedirectToAction("DSSV", new { KId = sinhVienThuocKhoa.KhoaId });
            }
            return View(sinhVienThuocKhoa);
        }

        // GET: SinhVienThuocKhoa/Edit/5
        public ActionResult Edit(decimal KId = 0, string SVId = null)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(KId, SVId);
                if (sinhVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == KId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.TenKhoa = kh.TenKhoa;
                    ViewBag.IdKhoa = kh.KhoaId;
                    ViewBag.KhoaID = new SelectList(db.Khoas, "KhoaId", "TenKhoa", kh.KhoaId);
                }
                else
                {
                    ViewBag.KhoaID = new SelectList(db.Khoas, "KhoaId", "TenKhoa");
                }
                ViewBag.KhoaID = new SelectList(db.Khoas, "KhoaId", "TenKhoa", sinhVienThuocKhoa.KhoaId);
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocKhoa.SinhVienId);
                return View(sinhVienThuocKhoa);
            }
            
        }

        // POST: SinhVienThuocKhoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhoaId,SinhVienId,TuNgay,DenNgay")] SinhVienThuocKhoa sinhVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVienThuocKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSSV", new { KId = sinhVienThuocKhoa.KhoaId });
            }
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", sinhVienThuocKhoa.KhoaId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", sinhVienThuocKhoa.SinhVienId);
            return View(sinhVienThuocKhoa);
        }

        // GET: SinhVienThuocKhoa/Delete/5
        public ActionResult Delete(decimal? KId, string SVId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (KId == 0 || SVId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(KId, SVId);
                if (sinhVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == KId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoa = kh.KhoaId;
                    ViewBag.TenKhoa = kh.TenKhoa;
                }
                return View(sinhVienThuocKhoa);
            }
            
        }

        // POST: SinhVienThuocKhoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? KId, string SVId)
        {
            SinhVienThuocKhoa sinhVienThuocKhoa = db.SinhVienThuocKhoas.Find(KId, SVId);
            db.SinhVienThuocKhoas.Remove(sinhVienThuocKhoa);
            db.SaveChanges();
            return RedirectToAction("DSSV", new { KId = sinhVienThuocKhoa.KhoaId });
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
