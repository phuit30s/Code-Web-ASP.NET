using DryFood.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DryFood.Controllers
{
    public class ProfileController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("DangNhap", "KhachHang");
            TAIKHOAN khach = Session["TaiKhoan"] as TAIKHOAN; //Khách
            return View(khach);
        }

    }
}