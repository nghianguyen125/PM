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

namespace ProjectManagement.Controllers.Admin
{
    public class GiangVienController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();

        // GET: GiangVien
        [HttpGet]
        public ActionResult Index(string searchString, int? page, string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            var giangvien = from b in db.GiangViens select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                giangvien = db.GiangViens.Where(s => s.HoTen.Contains(searchString));
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
            return View(giangvien.OrderBy(s => s.HoTen).ToList().ToPagedList(pageNumber, pageSize));
        }

 

  
        // GET: GiangVien/Create
        public ActionResult Create()
        {
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", "GioiTinh");
            return View();
        }

        // POST: GiangVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GiangVienId,HoTen,GioiTinh")] GiangVien giangvien)
        {
            if (ModelState.IsValid)
            {
                db.GiangViens.Add(giangvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giangvien);
        }

        // GET: Nganh/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangvien = db.GiangViens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", giangvien.GiangVienId);
            return View(giangvien);
        }

        // POST: Nganh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GiangVienId,HoTen,GioiTinh")] GiangVien giangvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiangVienId = new SelectList(db.GiangViens, "GiangVienId", "HoTen", giangvien.GiangVienId);
            return View(giangvien);
        }

        // GET: GiangVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangvien = db.GiangViens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Nganh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GiangVien giangvien = db.GiangViens.Find(id);
            db.GiangViens.Remove(giangvien);
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
