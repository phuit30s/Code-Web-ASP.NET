using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;

namespace DryFood.Controllers
{
    public class DryFoodController : Controller
    {
        dryfoodEntities db = new dryfoodEntities();

        // GET: DryFood
        public ActionResult Index()
        {
            var dsSanPhamMoi = LaySanPhamMoi(5);
            return View(dsSanPhamMoi);
        }
        private List<SANPHAM> LaySanPhamMoi(int soluong)
        {
            return db.SANPHAM.OrderByDescending(sp => sp.NgayNhap).Take(soluong).ToList();
        }
        private List<SANPHAM> LaySanPhamHot(int soluong)
        {
            return db.SANPHAM.OrderByDescending(sp => sp.SoLanBan).Take(soluong).ToList();
        }
        public ActionResult DanhSachSanPham()
        {
            return View(db.SANPHAM.ToList());
        }
        public ActionResult Details(int id)
        {
            var sanPham = db.SANPHAM.FirstOrDefault(s => s.MaSP == id);
            //ViewBag.Category= new 
            return View(sanPham);
        }
        public ActionResult LayLoai()
        {
            var dsLoai = db.LOAI.ToList();
            return PartialView(dsLoai);
        }
        public ActionResult SPTheoLoai(int id)
        {
            var dsSPTheoLoai = db.SANPHAM.Where(sp => sp.MaLoai == id).ToList();
            return View("DanhSachSanPham", dsSPTheoLoai);
        }

        //Tìm kiếm
        // GET: Link
        public ActionResult Index(string searchString)
        {
            var links = from l in db.SANPHAM // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenSP.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return View(links);
        }
    }
}