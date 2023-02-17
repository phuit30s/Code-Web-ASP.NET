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
    public class TAIKHOANsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/TAIKHOANs
        public ActionResult Index()
        {
            var tAIKHOAN = db.TAIKHOAN.Include(t => t.KHACHHANG);
            return View(tAIKHOAN.ToList());
        }

        // GET: Admin/TAIKHOANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // GET: Admin/TAIKHOANs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho");
            return View();
        }

        // POST: Admin/TAIKHOANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,MatKhau,SDT,DiemTichLuy,Roles")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.TAIKHOAN.Add(tAIKHOAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", tAIKHOAN.MaKH);
            return View(tAIKHOAN);
        }

        // GET: Admin/TAIKHOANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", tAIKHOAN.MaKH);
            return View(tAIKHOAN);
        }

        // POST: Admin/TAIKHOANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,MatKhau,SDT,DiemTichLuy,Roles")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", tAIKHOAN.MaKH);
            return View(tAIKHOAN);
        }

        // GET: Admin/TAIKHOANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: Admin/TAIKHOANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            db.TAIKHOAN.Remove(tAIKHOAN);
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
