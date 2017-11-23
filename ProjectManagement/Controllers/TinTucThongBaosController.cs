using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    public class TinTucThongBaosController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: TinTucThongBaos
        public ActionResult Index()
        {
            var tinTucThongBaos = db.TinTucThongBaos.Include(t => t.TaiKhoan);
            return View(tinTucThongBaos.ToList());
        }

        // GET: TinTucThongBaos/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTucThongBao = db.TinTucThongBaos.Find(id);
            if (tinTucThongBao == null)
            {
                return HttpNotFound();
            }
            return View(tinTucThongBao);
        }

        // GET: TinTucThongBaos/Create
        public ActionResult Create()
        {
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username");
            return View();
        }

        // POST: TinTucThongBaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TinTucId,TieuDe,NoiDung,NgayDang,TaiKhoanId")] TinTucThongBao tinTucThongBao)
        {
            if (ModelState.IsValid)
            {
                db.TinTucThongBaos.Add(tinTucThongBao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username", tinTucThongBao.TaiKhoanId);
            return View(tinTucThongBao);
        }

        // GET: TinTucThongBaos/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTucThongBao = db.TinTucThongBaos.Find(id);
            if (tinTucThongBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username", tinTucThongBao.TaiKhoanId);
            return View(tinTucThongBao);
        }

        // POST: TinTucThongBaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TinTucId,TieuDe,NoiDung,NgayDang,TaiKhoanId")] TinTucThongBao tinTucThongBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTucThongBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaiKhoanId = new SelectList(db.TaiKhoans, "TaiKhoanId", "Username", tinTucThongBao.TaiKhoanId);
            return View(tinTucThongBao);
        }

        // GET: TinTucThongBaos/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucThongBao tinTucThongBao = db.TinTucThongBaos.Find(id);
            if (tinTucThongBao == null)
            {
                return HttpNotFound();
            }
            return View(tinTucThongBao);
        }

        // POST: TinTucThongBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TinTucThongBao tinTucThongBao = db.TinTucThongBaos.Find(id);
            db.TinTucThongBaos.Remove(tinTucThongBao);
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
