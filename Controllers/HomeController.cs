using DryFood.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DryFood.Controllers
{
    public class HomeController : Controller
    {
        dryfoodEntities db = new dryfoodEntities();

        // GET: DryFood
        public ActionResult Index(string Searchstring = "")
        {
            var dsSanPhamMoi = LaySanPhamMoi(5);
            if (Searchstring != "")
            {
                var sanPhams = db.SANPHAM.Where(x => x.TenSP.ToUpper().Contains(Searchstring.ToUpper()));
                return View(sanPhams.ToList());
            }
            return View(dsSanPhamMoi);
        }
        private List<SANPHAM> LaySanPhamMoi(int soluong)
        {
            return db.SANPHAM.OrderByDescending(sp => sp.NgayNhap).Where(sp => sp.SoLuong > 0).Take(soluong).ToList();
        }
        public ActionResult LaySanPhamHot()
        {
            var dsSanPhamHot = LaySanPhamHot(5);
            return PartialView(dsSanPhamHot);
        }
        private List<SANPHAM> LaySanPhamHot(int soluong)
        {
            return db.SANPHAM.OrderByDescending(sp => sp.SoLanBan).Where(sp => sp.SoLuong > 0).Take(soluong).ToList();
        }
        public ActionResult DanhSachSanPham()
        {
            return View(db.SANPHAM.ToList());  
        }
        public ActionResult Details(int id)
        {
            var sanPham = db.SANPHAM.FirstOrDefault(s => s.MaSP == id);
            return View(sanPham);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {

            return View();
        }

    }
}