﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.Admin
{
    public class DeTaiController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: DeTai
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.DeTais.ToList());
            }
        }

        public ActionResult TaiLieu(decimal? DTid)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.TaiLieux.ToList());
            }
        }
        
        // GET: DeTai/Details/5
        public ActionResult Details(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DeTai deTai = db.DeTais.Find(id);
                if (deTai == null)
                {
                    return HttpNotFound();
                }
                return View(deTai);
            }
        }

        // GET: DeTai/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else return View();
        }

        // POST: DeTai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeTaiId,TenDeTai,MoTa,SoLuongThanhVien,NgayTao,NgayDangKy")] DeTai deTai)
        {
                if (ModelState.IsValid)
                {
                    db.DeTais.Add(deTai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(deTai);
        }

        // GET: DeTai/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DeTai deTai = db.DeTais.Find(id);
                if (deTai == null)
                {
                    return HttpNotFound();
                }
                return View(deTai);
            }
        }

        // POST: DeTai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeTaiId,TenDeTai,MoTa,SoLuongThanhVien,NgayTao,NgayDangKy")] DeTai deTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deTai);
        }

        // GET: DeTai/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DeTai deTai = db.DeTais.Find(id);
                if (deTai == null)
                {
                    return HttpNotFound();
                }
                return View(deTai);
            }
        }

        // POST: DeTai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DeTai deTai = db.DeTais.Find(id);
            db.DeTais.Remove(deTai);
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
