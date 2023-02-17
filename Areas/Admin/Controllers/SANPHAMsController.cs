using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;

namespace DryFood.Areas.Admin.Controllers
{
    public class SANPHAMsController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index()
        {
            var sANPHAM = db.SANPHAM.Include(s => s.LOAI).Include(s => s.NHACUNGUNG);
            return View(sANPHAM.ToList());
        }

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LOAI, "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NHACUNGUNG, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,SoLuong,GiaBan,AnhSP,NgayNhap,SoLanBan,TrangThaiSP,MaLoai,MaNCC")] SANPHAM sANPHAM, HttpPostedFileBase AnhSPs)
        {
            if (ModelState.IsValid)
            {
                int dem = 1;
                foreach (var item in db.SANPHAM)
                {
                    if (item.MaSP == dem)
                        dem++;
                }
                if (AnhSPs != null)
                {
                    string _FileName = Path.GetFileName(AnhSPs.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    AnhSPs.SaveAs(_path);
                }
                sANPHAM.MaSP = dem;
                sANPHAM.AnhSP = AnhSPs.FileName;
                db.SANPHAM.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LOAI, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGUNG, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LOAI, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGUNG, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "MaSP,TenSP,MoTa,SoLuong,GiaBan,AnhSP,NgayNhap,SoLanBan,TrangThaiSP,MaLoai,MaNCC")] SANPHAM sANPHAM,
            HttpPostedFileBase AnhSPs)
        {
            if (ModelState.IsValid)
            {
                if (AnhSPs != null)
                {
                    string _FileName = Path.GetFileName(AnhSPs.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    AnhSPs.SaveAs(_path);
                }

                sANPHAM.AnhSP = AnhSPs.FileName;
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LOAI, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NHACUNGUNG, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            db.SANPHAM.Remove(sANPHAM);
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
