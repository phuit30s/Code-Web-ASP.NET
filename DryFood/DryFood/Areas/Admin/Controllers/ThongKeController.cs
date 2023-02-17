using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;

namespace DryFood.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();

        // GET: Admin/ThongKe
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
            decimal TongTien = db.CTDONHANG.Sum(n => n.TongTienSP).Value;
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

        public ActionResult ThongKeTheoNgay(DateTime? firstDate, DateTime? lastDate)
        {
            var donHang = db.DONHANG.ToList();
            ViewBag.FirstDate = firstDate;
            ViewBag.LastDate = lastDate;
            if (firstDate != null && lastDate != null)
            {
                var dh = db.DONHANG.Where(dha => dha.NgayDat <= lastDate && dha.NgayDat >= firstDate).ToList();
                ViewBag.Total = String.Format("{0:0,00}", dh.Sum(dha => dha.ThanhTien));
                return View(dh);
            }
            ViewBag.Total = String.Format("{0:0,00}", db.DONHANG.Sum(dha => dha.ThanhTien));
            return View(donHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}