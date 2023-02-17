using DryFood.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DryFood.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        private dryfoodEntities db = new dryfoodEntities();

        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiTruyCapOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDH = ThongKeDonHang();
            ViewBag.TongTK = ThongKeThanhVien();
            return View();
        }

        public double ThongKeDonHang()
        {
            double slDH = db.DONHANG.Count();
            return slDH;
        }

        public double ThongKeThanhVien()
        {
            double slTV = db.TAIKHOAN.Count();
            return slTV;
        }

        public decimal ThongKeDoanhThu()
        {
            decimal TongTien = 0;
            var CTDH = db.CTDONHANG;
            if (CTDH.Count() > 0)
                TongTien = db.CTDONHANG.Sum(n => n.TongTienSP).Value;
            return TongTien;
        }

        public decimal ThongKeDoanhThuTheoThang(int thang, int nam)
        {
            var listDH = db.DONHANG.Where(n => n.NgayDat.Month == thang && n.NgayDat.Year == nam);
            decimal TongTien = 0;
            foreach (var item in listDH)
            {
                TongTien += item.CTDONHANG.Sum(n => n.TongTienSP).Value;
            }
            return TongTien;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}