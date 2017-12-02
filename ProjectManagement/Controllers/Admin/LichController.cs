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
    public class LichController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: Lich
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else return View(db.QuanLyLiches.ToList());
        }

        // GET: Lich/Details/5
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
                QuanLyLich lich = db.QuanLyLiches.Find(id);
                if (lich == null)
                {
                    return HttpNotFound();
                }
                return View(lich);
            }

        }

        // GET: Lich/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "Admin");
            }
            else return View();
        }

        // POST: Lich/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LichId,MocNgay")] QuanLyLich lich)
        {
            if (ModelState.IsValid)
            {
                db.QuanLyLiches.Add(lich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lich);
        }

        // GET: Lich/Edit/5
        public ActionResult Edit(decimal id)
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
                QuanLyLich lich = db.QuanLyLiches.Find(id);
                if (lich == null)
                {
                    return HttpNotFound();
                }
                return View(lich);
            }

        }

        // POST: Lich/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LichId,MocNgay")] QuanLyLich lich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lich);
        }

        // GET: Lich/Delete/5
        public ActionResult Delete(decimal id)
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
                QuanLyLich lich = db.QuanLyLiches.Find(id);
                if (lich == null)
                {
                    return HttpNotFound();
                }
                return View(lich);
            }

        }

        // POST: Lich/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            QuanLyLich lich = db.QuanLyLiches.Find(id);
            db.QuanLyLiches.Remove(lich);
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
