using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;

namespace ProjectManagement.Controllers.Admin
{
    public class GiangVienThuocKhoaController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: GiangVienThuocKhoa
        [HttpGet]
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter, string option)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                var giangVienThuocKhoa = db.GiangVienThuocKhoas.Include(g => g.GiangVien);
                var giangVienThuocKhoa1 = db.GiangVienThuocKhoas.Include(g => g.Khoa);
                if (!String.IsNullOrEmpty(searchString))
                {
                    if (option == "Khoa")
                    {
                        giangVienThuocKhoa = db.GiangVienThuocKhoas.Where(s => s.Khoa.TenKhoa.Contains(searchString));
                    }
                    else if (option == "GiangVien")
                    {
                        giangVienThuocKhoa = db.GiangVienThuocKhoas.Where(s => s.GiangVien.HoTen.Contains(searchString));
                    }
                    else
                    {
                        giangVienThuocKhoa = db.GiangVienThuocKhoas.Where(s => (s.GiangVien.HoTen + s.Khoa.TenKhoa).Contains(searchString));
                    }
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
                return View(giangVienThuocKhoa.OrderBy(s => s.Khoa.TenKhoa + s.GiangVien.HoTen).ToList().ToPagedList(pageNumber, pageSize));
            }
                
        }

        // GET: GiangVienThuocKhoa/Details/5
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
                GiangVienThuocKhoa giangVienThuocKhoa = db.GiangVienThuocKhoas.Find(id);
                if (giangVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(giangVienThuocKhoa);
            }

        }

        // GET: GiangVienThuocKhoa/Create
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
                var list1 = db.GiangViens.ToList();
                var list2 = db.GiangVienThuocKhoas.Where(p => p.KhoaId == KId).ToList();
                var sv = list1.Where(p => !list2.Any(p2 => p2.GiangVienId == p.GiangVienId)).ToList();

                ViewBag.GiangVienId = new SelectList(sv, "GiangVienId", "HoTen");
                return View();
            }

        }

        // POST: GiangVienThuocKhoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhoaId,GiangVienId,TuNgay,DenNgay")] GiangVienThuocKhoa giangVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                var svKh = db.GiangVienThuocKhoas.Where(n => n.KhoaId == giangVienThuocKhoa.KhoaId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.Khoas.Where(n => n.KhoaId == giangVienThuocKhoa.KhoaId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdKhoaHoc = kh.KhoaId;
                    ViewBag.TenKhoaHoc = kh.TenKhoa;
                }
                db.GiangVienThuocKhoas.Add(giangVienThuocKhoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", giangVienThuocKhoa.GiangVienId);
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", giangVienThuocKhoa.KhoaId);
            return View(giangVienThuocKhoa);
        }

        // GET: GiangVienThuocKhoa/Edit/5
        public ActionResult Edit(decimal kid, string gvid, DateTime tn)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (kid == 0 && gvid == null && tn == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GiangVienThuocKhoa giangVienThuocKhoa = db.GiangVienThuocKhoas.Find(kid, gvid, tn);
                if (giangVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", giangVienThuocKhoa.GiangVienId);
                ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", giangVienThuocKhoa.KhoaId);
                return View(giangVienThuocKhoa);
            }

        }

        // POST: GiangVienThuocKhoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Khoa.TenKhoa,GiangVien.HoTen,TuNgay,DenNgay,KhoaId,GiangVienId")] GiangVienThuocKhoa giangVienThuocKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangVienThuocKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", giangVienThuocKhoa.GiangVienId);
            ViewBag.KhoaId = new SelectList(db.Khoas, "KhoaId", "TenKhoa", giangVienThuocKhoa.KhoaId);
            return View(giangVienThuocKhoa);
        }

        // GET: GiangVienThuocKhoa/Delete/5
        public ActionResult Delete(decimal kid, string gvid, DateTime tn)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (kid == 0 && gvid == null && tn == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GiangVienThuocKhoa giangVienThuocKhoa = db.GiangVienThuocKhoas.Find(kid, gvid, tn);
                if (giangVienThuocKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(giangVienThuocKhoa);
            }

        }

        // POST: GiangVienThuocKhoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal kid, string gvid, DateTime tn)
        {
            GiangVienThuocKhoa giangVienThuocKhoa = db.GiangVienThuocKhoas.Find(kid, gvid, tn);
            db.GiangVienThuocKhoas.Remove(giangVienThuocKhoa);
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
