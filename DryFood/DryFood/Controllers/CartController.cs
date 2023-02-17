using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DryFood.Areas.Admin.Data;
using DryFood.Models;

namespace DryFood.Controllers
{
    public class CartController : Controller
    {
        private dryfoodEntities db = new dryfoodEntities();
        //Lấy giỏ hàng
        public List<CartItem> Laygiohang()
        {
            List<CartItem> lstGiohang = Session["Giohang"] as List<CartItem>;
            if(lstGiohang==null)
            {
                lstGiohang = new List<CartItem>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ThongTinDatHang LayThongTin()
        {
            ThongTinDatHang thongTin = Session["ThongTin"] as ThongTinDatHang;
            if (thongTin == null)
            {
                thongTin = new ThongTinDatHang();
                Session["ThongTin"] = thongTin;
            }
            return thongTin;
        }
        // GET: GioHang
        [HttpGet]
        public ActionResult Index()
        {
            //if (Session["TaiKhoan"] == null)
            //    return RedirectToAction("DangNhap", "KhachHang");
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }
        [HttpPost]
        public ActionResult Index(ThongTinDatHang thongTinDatHang)
        {
            if (ModelState.IsValid)
            {
                if (Session["ThongTin"] != null)
                {
                    Session["ThongTin"] = new ThongTinDatHang();
                }

                ThongTinDatHang ttdh = Session["ThongTin"] as ThongTinDatHang;

                ThongTinDatHang thongTin = new ThongTinDatHang() 
                { 
                    SDT = thongTinDatHang.SDT,
                    Ten = thongTinDatHang.Ten,
                    DiaChi = thongTinDatHang.DiaChi,
                    MaKM = thongTinDatHang.MaKM,
                    MaPTTT = thongTinDatHang.MaPTTT
                };
                
                ttdh = thongTin; 
                
            }
            return RedirectToAction("Dathang");
        }
        public HttpStatusCodeResult ThemVaoGio(int SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.MaSP == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                SANPHAM sp = db.SANPHAM.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem newItem = new CartItem()
                {
                    MaSP = SanPhamID,
                    TenSP = sp.TenSP,
                    SoLuong = 1,
                    AnhSP = sp.AnhSP,
                    Giaban = sp.GiaBan

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.MaSP == SanPhamID);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return new HttpStatusCodeResult(204);
        }
        public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.MaSP == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");

        }
        //Dathang
        public ActionResult Dathang()
        {
            if (Session["Giohang"] == null)
                return RedirectToAction("Index", "Home");
            /*if (Session["ThongTin"] == null)
                return RedirectToAction("Index");*/
            //Lay gio hang từ session
            List<CartItem> lstGiohang = Laygiohang();
            return View(lstGiohang);
        }
        
        public PartialViewResult PhieuThongTin()
        {
            ThongTinDatHang thongTin = LayThongTin();
            return PartialView(thongTin);
        }

        public ActionResult DongYDat()
        {
            TAIKHOAN khach = Session["TaiKhoan"] as TAIKHOAN;
            ThongTinDatHang thongTin = LayThongTin();
            List<CartItem> carts = Laygiohang(); //Giỏ hàng
            DONHANG donhang = new DONHANG(); //Tạo mới đơn đặt hàng
            if (ModelState.IsValid)
            {
                int demdh = 1;
                int demkh = 1;

                foreach (var item in db.DONHANG)
                {
                    if (item.MaDH == demdh)
                        demdh++;
                }

                if (khach == null)
                {
                    foreach (var item in db.KHACHHANG)
                    {
                        if (item.SDT == thongTin.SDT)
                        {
                            donhang.MaKH = item.MaKH;
                            break;
                        }
                        if (item.MaKH == demkh)
                            demkh++;
                    }
                    KHACHHANG khachhang = new KHACHHANG();
                    var kh = db.KHACHHANG.FirstOrDefault(s => s.MaKH == demkh);
                    if (kh == null)
                    {
                        khachhang.MaKH = demkh;
                        khachhang.Ho = "";
                        khachhang.TenDem = "";
                        khachhang.Ten = thongTin.Ten;
                        khachhang.SDT = thongTin.SDT;
                        khachhang.DiaChi = thongTin.DiaChi;

                        db.KHACHHANG.Add(khachhang);
                        db.SaveChanges();
                    }
                }
                else
                {
                    donhang.MaKH = khach.MaKH;
                    donhang.NoiGiao = khach.KHACHHANG.DiaChi;
                }

                donhang.MaDH = demdh;
                donhang.NgayDat = DateTime.Now;
                donhang.NhanXet = "";

                if (thongTin.MaKM != 0)
                    donhang.MaKM = thongTin.MaKM;

                donhang.MaPTTT = thongTin.MaPTTT;
                donhang.MaTrangThai = 0;
                donhang.ThanhTien = 0;
                db.DONHANG.Add(donhang);
                db.SaveChanges();
                decimal tongTien = 0;

                foreach (var item in carts)
                {
                    CTDONHANG chitiet = new CTDONHANG();
                    chitiet.MaDH = donhang.MaDH;
                    chitiet.MaSP = item.MaSP;
                    chitiet.SoLuong = item.SoLuong;
                    chitiet.GiaMua = item.Giaban;
                    chitiet.TongTienSP = item.ThanhTien;
                    tongTien += item.ThanhTien;

                    var sp = db.SANPHAM.FirstOrDefault(s => s.MaSP == item.MaSP);
                    sp.SoLanBan++;
                    sp.SoLuong -= item.SoLuong;

                    db.CTDONHANG.Add(chitiet);
                }

                var dh1 = db.DONHANG.FirstOrDefault(k => k.MaDH == demdh);
                dh1.ThanhTien = tongTien;
                if(khach != null) khach.DiemTichLuy = (int)tongTien / 10000;
                db.SaveChanges();

            }

            Session["Giohang"] = null;
            return RedirectToAction("HoanThanhDat");
        }

        //Đặt hàng
        //public ActionResult Dathang(FormCollection collection)
        //{
        //    //Thêm đơn hàng
        //    DONHANG ddh = new DONHANG();


        //    KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
        //    //KHACHHANG kh = Session["Khachhang"] as KHACHHANG;

        //    List<CartItem> gh = Laygiohang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDat = DateTime.Now;
        //    var ngaygiao = string.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
        //    //ddh.ng= DateTime.Parse(ngaygiao); Ngày giao
        //    //data.DONHANG.InsertOnSubmit(ddh);
        //    //ddh.
        //    //Thêm chi tiết đơn hàng 
        //    foreach(var item in gh)
        //    {
        //        CTDONHANG ctdh = new CTDONHANG();
        //        ctdh.MaDH = ddh.MaDH;
        //        ctdh.MaSP = item.MaSP;
        //        ctdh.SoLuong = item.SoLuong;
        //        ctdh.GiaMua = (decimal)item.Giaban;

        //    }
        //    Session["Giohang"] = null;
        //    return RedirectToAction("DongYDat", "Cart");
        //}
        //Xóa hàng
        public ActionResult XoaGiohang(int iMaSp)
        {
            List<CartItem> lstGiohang = Laygiohang();
            CartItem sanpham = lstGiohang.SingleOrDefault(m => m.MaSP == iMaSp);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(m => m.MaSP == iMaSp);

                return RedirectToAction("Index");
            }
            if(lstGiohang.Count==0)
            {
                return RedirectToAction("Index","Cart");
            }
            return RedirectToAction("Index");

        }

        public ActionResult HoanThanhDat()
        {
            return View();
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             