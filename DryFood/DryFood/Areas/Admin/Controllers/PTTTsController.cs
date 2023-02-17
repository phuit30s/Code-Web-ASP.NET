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
    public class PTTTsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/PTTTs
        public ActionResult Index()
        {
            return View(db.PTTT.ToList());
        }

        // GET: Admin/PTTTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTTT pTTT = db.PTTT.Find(id);
            if (pTTT == null)
            {
                return HttpNotFound();
            }
            return View(pTTT);
        }

        // GET: Admin/PTTTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PTTTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPTTT,TenPTTT,AnhPTTT")] PTTT pTTT)
        {
            if (ModelState.IsValid)
            {
                db.PTTT.Add(pTTT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pTTT);
        }

        // GET: Admin/PTTTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTTT pTTT = db.PTTT.Find(id);
            if (pTTT == null)
            {
                return HttpNotFound();
            }
            return View(pTTT);
        }

        // POST: Admin/PTTTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPTTT,TenPTTT,AnhPTTT")] PTTT pTTT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pTTT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pTTT);
        }

        // GET: Admin/PTTTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTTT pTTT = db.PTTT.Find(id);
            if (pTTT == null)
            {
                return HttpNotFound();
            }
            return View(pTTT);
        }

        // POST: Admin/PTTTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PTTT pTTT = db.PTTT.Find(id);
            db.PTTT.Remove(pTTT);
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
