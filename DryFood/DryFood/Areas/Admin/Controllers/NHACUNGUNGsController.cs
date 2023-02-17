using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;

namespace DryFood.Areas.Admin.Controllers
{
    public class NHACUNGUNGsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/NHACUNGUNGs
        public ActionResult Index()
        {
            return View(db.NHACUNGUNG.ToList());
        }

        // GET: Admin/NHACUNGUNGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGUNG nHACUNGUNG = db.NHACUNGUNG.Find(id);
            if (nHACUNGUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGUNG);
        }

        // GET: Admin/NHACUNGUNGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NHACUNGUNGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,SDTNCC")] NHACUNGUNG nHACUNGUNG)
        {
            if (ModelState.IsValid)
            {
                db.NHACUNGUNG.Add(nHACUNGUNG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHACUNGUNG);
        }

        // GET: Admin/NHACUNGUNGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGUNG nHACUNGUNG = db.NHACUNGUNG.Find(id);
            if (nHACUNGUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGUNG);
        }

        // POST: Admin/NHACUNGUNGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,SDTNCC")] NHACUNGUNG nHACUNGUNG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHACUNGUNG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHACUNGUNG);
        }

        // GET: Admin/NHACUNGUNGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGUNG nHACUNGUNG = db.NHACUNGUNG.Find(id);
            if (nHACUNGUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGUNG);
        }

        // POST: Admin/NHACUNGUNGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHACUNGUNG nHACUNGUNG = db.NHACUNGUNG.Find(id);
            db.NHACUNGUNG.Remove(nHACUNGUNG);
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
