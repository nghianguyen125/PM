using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class PhanDeTaiChoDotKhoaLuanController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: PhanDeTaiChoDotKhoaLuan
        public ActionResult Index()
        {
            return View(db.DotKhoaLuans.ToList());
        }

        // GET: PhanDeTaiChoDotKhoaLuan/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoDotKhoaLuan phanDeTaiChoDotKhoaLuan = db.PhanDeTaiChoDotKhoaLuans.Find(id);
            if (phanDeTaiChoDotKhoaLuan == null)
            {
                return HttpNotFound();
            }
            return View(phanDeTaiChoDotKhoaLuan);
        }

        public ActionResult DSDT(decimal? DId)
        {
            if (DId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dotKhoaLuan = db.PhanDeTaiChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).ToList();
            if (dotKhoaLuan == null)
            {
                return HttpNotFound();
            }
            var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
            if (kh != null)
            {
                ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
            }

            //var sinhVienKhoaHocs = db.SinhVienKhoaHocs.Include(s => s.KhoaHoc).Include(s => s.SinhVien).Where(n => (n.KhoaHocID == KHId && n.SinhVienId == SVId));
            return View(dotKhoaLuan.ToList());
        }

        // GET: PhanDeTaiChoDotKhoaLuan/Create
        public ActionResult Create(decimal? DId)
        {
            if (DId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var phanDeTai = db.PhanDeTaiChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).ToList();
            if (phanDeTai == null)
            {
                return HttpNotFound();
            }
            var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
            if (kh != null)
            {
                ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", kh.DotKhoaLuanId);
            }
            else
            {
                ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
            return View();
        }

        // POST: PhanDeTaiChoDotKhoaLuan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DotKhoaLuanId,DeTaiId,NgayPhanDeTai")] PhanDeTaiChoDotKhoaLuan phanDeTaiChoDotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                db.PhanDeTaiChoDotKhoaLuans.Add(phanDeTaiChoDotKhoaLuan);
                db.SaveChanges();
                var svKh = db.PhanDeTaiChoDotKhoaLuans.Where(n => n.DotKhoaLuanId == phanDeTaiChoDotKhoaLuan.DotKhoaLuanId).ToList();
                if (svKh == null)
                {
                    return HttpNotFound();
                }
                var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == phanDeTaiChoDotKhoaLuan.DotKhoaLuanId).SingleOrDefault();
                if (kh != null)
                {
                    ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                    ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                }
                return RedirectToAction("DSDT", new { DId = phanDeTaiChoDotKhoaLuan.DotKhoaLuanId });
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoDotKhoaLuan.DeTaiId);
            ViewBag.DotKhoaLuanId = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", phanDeTaiChoDotKhoaLuan.DotKhoaLuanId);
            return View(phanDeTaiChoDotKhoaLuan);
        }

        // GET: PhanDeTaiChoDotKhoaLuan/Edit/5
        public ActionResult Edit(decimal? DId, decimal? DTId)
        {
            if (DId == 0 || DTId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoDotKhoaLuan phanDeTai = db.PhanDeTaiChoDotKhoaLuans.Find(DId, DTId);
            if (phanDeTai == null)
            {
                return HttpNotFound();
            }
            var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
            if (kh != null)
            {
                ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
                ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", kh.DotKhoaLuanId);
            }
            else
            {
                ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan");
            }
            ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", phanDeTai.DotKhoaLuanId);
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTai.DeTaiId);
            return View(phanDeTai);
        }

        // POST: PhanDeTaiChoDotKhoaLuan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DotKhoaLuanId,DeTaiId,NgayPhanDeTai")] PhanDeTaiChoDotKhoaLuan phanDeTaiChoDotKhoaLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanDeTaiChoDotKhoaLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSDT", new { DId = phanDeTaiChoDotKhoaLuan.DotKhoaLuanId });
            }
            ViewBag.DotKhoaLuanID = new SelectList(db.DotKhoaLuans, "DotKhoaLuanId", "TenDotKhoaLuan", phanDeTaiChoDotKhoaLuan.DotKhoaLuanId);
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoDotKhoaLuan.DeTaiId);
            return View(phanDeTaiChoDotKhoaLuan);
        }

        // GET: PhanDeTaiChoDotKhoaLuan/Delete/5
        public ActionResult Delete(decimal? DId, decimal? DTId)
        {
            if (DId == 0 || DTId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanDeTaiChoDotKhoaLuan phanDeTai = db.PhanDeTaiChoDotKhoaLuans.Find(DId, DTId);
            if (phanDeTai == null)
            {
                return HttpNotFound();
            }
            var kh = db.DotKhoaLuans.Where(n => n.DotKhoaLuanId == DId).SingleOrDefault();
            if (kh != null)
            {
                ViewBag.IdDotKhoaLuan = kh.DotKhoaLuanId;
                ViewBag.TenDotKhoaLuan = kh.TenDotKhoaLuan;
            }
            return View(phanDeTai);
        }

        // POST: PhanDeTaiChoDotKhoaLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? DId, string DTId)
        {
            PhanDeTaiChoDotKhoaLuan phanDeTaiChoDotKhoaLuan = db.PhanDeTaiChoDotKhoaLuans.Find(DId, DTId);
            db.PhanDeTaiChoDotKhoaLuans.Remove(phanDeTaiChoDotKhoaLuan);
            db.SaveChanges();
            return RedirectToAction("DSDT", new { DId = phanDeTaiChoDotKhoaLuan.DotKhoaLuanId });
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
