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
    public class DONHANGsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/DONHANGs
        public ActionResult Index()
        {
            var dONHANG = db.DONHANG.Include(d => d.KHACHHANG).Include(d => d.KHUYENMAI).Include(d => d.PTTT);
            return View(dONHANG.ToList());
        }

        // GET: Admin/DONHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANG.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho");
            ViewBag.MaKM = new SelectList(db.KHUYENMAI, "MaKM", "TenKM");
            ViewBag.MaPTTT = new SelectList(db.PTTT, "MaPTTT", "TenPTTT");
            return View();
        }

        // POST: Admin/DONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "MaDH,NoiGiao,NgayDat,NhanXet,MaKH,MaKM,MaPTTT,TamTinh,ThanhTien,MaTrangThai")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                int dem = 1;
                foreach (var item in db.DONHANG)
                {
                    if (item.MaDH == dem)
                        dem++;
                }
                dONHANG.MaDH = dem;
                db.DONHANG.Add(dONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", dONHANG.MaKH);
            ViewBag.MaKM = new SelectList(db.KHUYENMAI, "MaKM", "TenKM", dONHANG.MaKM);
            ViewBag.MaPTTT = new SelectList(db.PTTT, "MaPTTT", "TenPTTT", dONHANG.MaPTTT);
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANG.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", dONHANG.MaKH);
            ViewBag.MaKM = new SelectList(db.KHUYENMAI, "MaKM", "TenKM", dONHANG.MaKM);
            ViewBag.MaPTTT = new SelectList(db.PTTT, "MaPTTT", "TenPTTT", dONHANG.MaPTTT);
            return View(dONHANG);
        }

        // POST: Admin/DONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "MaDH,NoiGiao,NgayDat,NhanXet,MaKH,MaKM,MaPTTT,TamTinh,ThanhTien,MaTrangThai")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANG, "MaKH", "Ho", dONHANG.MaKH);
            ViewBag.MaKM = new SelectList(db.KHUYENMAI, "MaKM", "TenKM", dONHANG.MaKM);
            ViewBag.MaPTTT = new SelectList(db.PTTT, "MaPTTT", "TenPTTT", dONHANG.MaPTTT);
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANG.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: Admin/DONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONHANG dONHANG = db.DONHANG.Find(id);
            db.DONHANG.Remove(dONHANG);
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
