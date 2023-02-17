using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;
using DryFood.Models;

namespace DryFood.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        private dryfoodEntities db = new dryfoodEntities();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(DangKy dk)
        {
            KHACHHANG kh = new KHACHHANG();
            TAIKHOAN tk = new TAIKHOAN();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(dk.Ten))
                    ModelState.AddModelError(string.Empty, "Tên không được để trống");
                if (string.IsNullOrEmpty(dk.SDT))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(dk.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                var taikhoan = db.TAIKHOAN.FirstOrDefault(k => k.SDT == dk.SDT);
                if (taikhoan != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí số điện thoại này");
                if (ModelState.IsValid)
                {
                    int dem = 1;
                    foreach (var item in db.KHACHHANG)
                    {
                        if (item.MaKH == dem)
                            dem++;
                    }
                    kh.MaKH = dem;
                    kh.Ho = dk.Ho;
                    kh.TenDem = dk.TenDem;
                    kh.Ten = dk.Ten;
                    kh.SDT = dk.SDT;
                    kh.DiaChi = dk.DiaChi;

                    tk.MaKH = kh.MaKH;
                    tk.SDT = dk.SDT;
                    tk.MatKhau = dk.MatKhau;
                    tk.Roles = "client";

                    db.KHACHHANG.Add(kh);
                    db.TAIKHOAN.Add(tk);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(TAIKHOAN kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.SDT))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm khách hàng có tên đăng nhập và password hợp lệ trong CSDL
                    var khach = db.TAIKHOAN.FirstOrDefault(k => k.SDT == kh.SDT && k.MatKhau == kh.MatKhau);
                    if (khach != null)
                    {
                        if (khach.Roles == "Admin")
                        {
                            Session["TaiKhoan"] = khach;
                            return RedirectToAction("Index", "Admin");
                        }

                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        //Lưu vào session

                        Session["TaiKhoan"] = khach;

                    }
                    else
                    {
                        ViewBag.ThongBao = "Số điện thoại hoặc mật khẩu không đúng";
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuenMatKhau(DoiMatKhau dmk)
        {
            if (ModelState.IsValid)
            {
                var sdt = db.TAIKHOAN.FirstOrDefault(s => s.SDT == dmk.SDT);
                if(sdt != null)
                {
                    sdt.MatKhau = dmk.MatKhauMoi;
                    db.SaveChanges();
                    ViewBag.ThongBao = "Đã đổi mật khẩu";
                }
                else
                {
                    ViewBag.ThongBao = "Số điện thoại không tồn tại";
                    return View();
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}