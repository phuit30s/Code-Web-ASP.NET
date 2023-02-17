using DryFood.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DryFood.Models
{
    public class CartItem
    {
        dryfoodEntities db = new dryfoodEntities();
        public string AnhSP { get; set; }
        public int MaSP { get; set; }
        public string TenSP{ get; set; }
        public decimal Giaban { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien
        {

            get
            {
                return SoLuong * Giaban;
            }
        }
    }
 
}