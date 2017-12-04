using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class TaiLieuController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: TaiLieu
        public ActionResult Index()
        {
            var taiLieux = db.TaiLieux.Include(t => t.DeTai);
            return View(taiLieux.ToList());
        }

        // GET: TaiLieu/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            return View(taiLieu);
        }

        // GET: TaiLieu/Create
        public ActionResult Create(string id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Id = id;
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                decimal deTaiId = decimal.Parse(id);
                DeTai DeTaiId = db.DeTais.Where(x => x.DeTaiId == deTaiId).FirstOrDefault();
                if (DeTaiId == null)
                {
                    return RedirectToAction("Index");
                }
                //ViewBag.categoryName = category.CATEGORY_NAME;
                ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
            }
            return View();
        }

        // POST: TaiLieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaiLieuId,TenTaiLieu,TepTinDinhKem,DeTaiId")] TaiLieu taiLieu, String id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Id = id;
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", taiLieu.DeTaiId);
                return View(taiLieu);
            }
        }

        // GET: TaiLieu/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", taiLieu.DeTaiId);
            return View(taiLieu);
        }

        // POST: TaiLieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaiLieuId,TenTaiLieu,TepTinDinhKem,DeTaiId")] TaiLieu taiLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", taiLieu.DeTaiId);
            return View(taiLieu);
        }

        // GET: TaiLieu/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            return View(taiLieu);
        }

        // POST: TaiLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            db.TaiLieux.Remove(taiLieu);
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

        public string GetNewFileName(string a)
        {
            string fileName = "";
            if (a.Length > 0)
            {
                Random rnd = new Random();
                DateTime date = DateTime.Now;
                fileName = date.Day + "-" + date.Month + "-" + date.Year + "-" + date.Hour + "-" + date.Minute + "-" + date.Second + "-" + rnd.Next(1, 100) + Path.GetExtension(a);
            }
            return fileName;
        }
    }
}
