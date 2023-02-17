using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DryFood.Models
{
    public class ThongTinDatHang
    {
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public int MaKM { get; set; } = 0;
        public int MaPTTT { get; set; } = 1;
    }
}