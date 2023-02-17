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
    public class LOAIsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/LOAIs
        public ActionResult Index()
        {
            return View(db.LOAI.ToList());
        }

        // GET: Admin/LOAIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAI.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // GET: Admin/LOAIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoai,TenLoai")] LOAI lOAI)
        {
            if (ModelState.IsValid)
            {
                db.LOAI.Add(lOAI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAI);
        }

        // GET: Admin/LOAIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAI.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // POST: Admin/LOAIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoai,TenLoai")] LOAI lOAI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAI);
        }

        // GET: Admin/LOAIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAI.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // POST: Admin/LOAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOAI lOAI = db.LOAI.Find(id);
            db.LOAI.Remove(lOAI);
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
