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
    public class PhanDeTaiChoNhomSVController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: PhanDeTaiChoNhomSV
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var phanDeTaiChoNhomSVs = db.PhanDeTaiChoNhomSVs.Include(p => p.DeTai).Include(p => p.NhomSV);
                return View(phanDeTaiChoNhomSVs.ToList());

            }
                
        }

        // GET: PhanDeTaiChoNhomSV/Details/5
        public ActionResult Details(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PhanDeTaiChoNhomSV phanDeTaiChoNhomSV = db.PhanDeTaiChoNhomSVs.Find(id);
                if (phanDeTaiChoNhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(phanDeTaiChoNhomSV);
            }
            
        }

        // GET: PhanDeTaiChoNhomSV/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai");
                ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom");
                return View();
            }
                
        }

        // POST: PhanDeTaiChoNhomSV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhomSVId,DeTaiId,NgayPhanDeTai,GhiChu")] PhanDeTaiChoNhomSV phanDeTaiChoNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.PhanDeTaiChoNhomSVs.Add(phanDeTaiChoNhomSV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoNhomSV.DeTaiId);
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", phanDeTaiChoNhomSV.NhomSVId);
            return View(phanDeTaiChoNhomSV);
        }

        // GET: PhanDeTaiChoNhomSV/Edit/5
        public ActionResult Edit(decimal? NId, decimal? DTId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (NId == null || DTId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PhanDeTaiChoNhomSV phanDeTaiChoNhomSV = db.PhanDeTaiChoNhomSVs.Find(NId, DTId);
                if (phanDeTaiChoNhomSV == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoNhomSV.DeTaiId);
                ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", phanDeTaiChoNhomSV.NhomSVId);
                return View(phanDeTaiChoNhomSV);
            }
           
        }

        // POST: PhanDeTaiChoNhomSV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhomSVId,DeTaiId,NgayPhanDeTai,GhiChu")] PhanDeTaiChoNhomSV phanDeTaiChoNhomSV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanDeTaiChoNhomSV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeTaiId = new SelectList(db.DeTais, "DeTaiId", "TenDeTai", phanDeTaiChoNhomSV.DeTaiId);
            ViewBag.NhomSVId = new SelectList(db.NhomSVs, "NhomSVId", "TenNhom", phanDeTaiChoNhomSV.NhomSVId);
            return View(phanDeTaiChoNhomSV);
        }

        // GET: PhanDeTaiChoNhomSV/Delete/5
        public ActionResult Delete(decimal? NId, decimal? DTId)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (NId == 0 || DTId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PhanDeTaiChoNhomSV phanDeTaiChoNhomSV = db.PhanDeTaiChoNhomSVs.Find(NId, DTId);
                if (phanDeTaiChoNhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(phanDeTaiChoNhomSV);
            }
          
        }

        // POST: PhanDeTaiChoNhomSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal? NId, decimal? DTId)
        {
            PhanDeTaiChoNhomSV phanDeTaiChoNhomSV = db.PhanDeTaiChoNhomSVs.Find(NId, DTId);
            db.PhanDeTaiChoNhomSVs.Remove(phanDeTaiChoNhomSV);
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
