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

namespace ProjectManagement.Controllers
{
    public class DanhGiaDeTaisController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: DanhGiaDeTais
        public ActionResult Index()
        {
            var username = ProjectManagement.App_Start.Classes.UserManager.GetUserName;
            var tk = db.TaiKhoans.Where(z => z.Username == username).FirstOrDefault();
            if (tk.GiangVienId != null)
            {
                var gv = db.GiangViens.Where(a => a.GiangVienId == tk.GiangVienId).SingleOrDefault();
                ViewBag.giangVienId = gv.GiangVienId;
            }
            var danhGiaDeTais = db.DanhGiaDeTais.Include(d => d.DeTai).Include(d => d.SinhVien);
            return View(danhGiaDeTais.ToList());
        }

        // GET: DanhGiaDeTais/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(id);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var username = ProjectManagement.App_Start.Classes.UserManager.GetUserName;
                var tk = db.TaiKhoans.Where(z => z.Username == username).FirstOrDefault();

                var gv = db.GiangViens.Where(a => a.GiangVienId == tk.GiangVienId).FirstOrDefault();
                ViewBag.Ten = gv.HoTen;

                ViewBag.GiangVienId = gv.GiangVienId;
                ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
                ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen");
                return View();
            }
        }

        // POST: DanhGiaDeTais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeTaiId,SinhVienId,Diem,NoiDungDanhGia")] DanhGiaDeTai danhGiaDeTai)
        {
            if (ModelState.IsValid)
            {
                db.DanhGiaDeTais.Add(danhGiaDeTai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Edit/5
        public ActionResult Edit(decimal detaiid, string svid)
        {
            if (svid == null || detaiid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(detaiid, svid);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // POST: DanhGiaDeTais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeTaiId,SinhVienId,Diem,NoiDungDanhGia")] DanhGiaDeTai danhGiaDeTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhGiaDeTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", danhGiaDeTai.DeTaiId);
            ViewBag.SinhVienId = new SelectList(db.SinhViens, "SinhVienId", "HoTen", danhGiaDeTai.SinhVienId);
            return View(danhGiaDeTai);
        }

        // GET: DanhGiaDeTais/Delete/5
        public ActionResult Delete(decimal detaiid, string svid)
        {
            if (svid == null || detaiid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(detaiid, svid);
            if (danhGiaDeTai == null)
            {
                return HttpNotFound();
            }
            return View(danhGiaDeTai);
        }

        // POST: DanhGiaDeTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal detaiid, string svid)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                DanhGiaDeTai danhGiaDeTai = db.DanhGiaDeTais.Find(detaiid, svid);
                db.DanhGiaDeTais.Remove(danhGiaDeTai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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