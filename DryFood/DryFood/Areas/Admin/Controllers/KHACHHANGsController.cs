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
    public class KHACHHANGsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/KHACHHANGs
        public ActionResult Index()
        {
            var kHACHHANG = db.KHACHHANG.Include(k => k.TAIKHOAN);
            return View(kHACHHANG.ToList());
        }

        // GET: Admin/KHACHHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.TAIKHOAN, "MaKH", "MatKhau");
            return View();
        }

        // POST: Admin/KHACHHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,Ho,TenDem,Ten,SDT,DiaChi")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                int dem = 1;
                foreach (var item in db.KHACHHANG)
                {
                    if (item.MaKH == dem)
                        dem++;
                }
                kHACHHANG.MaKH = dem;
                db.KHACHHANG.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.TAIKHOAN, "MaKH", "MatKhau", kHACHHANG.MaKH);
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.TAIKHOAN, "MaKH", "MatKhau", kHACHHANG.MaKH);
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,Ho,TenDem,Ten,SDT,DiaChi")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.TAIKHOAN, "MaKH", "MatKhau", kHACHHANG.MaKH);
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            db.KHACHHANG.Remove(kHACHHANG);
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
