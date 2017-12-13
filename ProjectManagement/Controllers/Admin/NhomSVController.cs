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
    public class NhomSVController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: NhomSV
        public ActionResult Index()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(db.NhomSVs.ToList());
            }
                
        }

        // GET: NhomSV/Details/5
        public ActionResult Details(decimal Nid)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (Nid == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhomSV nhomSV = db.NhomSVs.Find(Nid);
                if (nhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(nhomSV);
            }
           
        }

        // GET: NhomSV/Create
        public ActionResult Create()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else return View();
        }

        // POST: NhomSV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NhomSVId,TenNhom")] NhomSV nhomSV)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.NhomSVs.Max(u => u.NhomSVId);
                nhomSV.NhomSVId = maxId + 1;
                db.NhomSVs.Add(nhomSV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhomSV);
        }

        // GET: NhomSV/Edit/5
        public ActionResult Edit(decimal Nid)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (Nid == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhomSV nhomSV = db.NhomSVs.Find(Nid);
                if (nhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(nhomSV);
            }
          
        }

        // POST: NhomSV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NhomSVId,TenNhom")] NhomSV nhomSV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhomSV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhomSV);
        }

        // GET: NhomSV/Delete/5
        public ActionResult Delete(decimal Nid)
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (Nid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhomSV nhomSV = db.NhomSVs.Find(Nid);
                if (nhomSV == null)
                {
                    return HttpNotFound();
                }
                return View(nhomSV);
            }
           
        }

        // POST: NhomSV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal Nid)
        {
            NhomSV nhomSV = db.NhomSVs.Find(Nid);
            db.NhomSVs.Remove(nhomSV);
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
