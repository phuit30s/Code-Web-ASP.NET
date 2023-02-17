using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DryFood.Models
{
    public class DoiMatKhau
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống", AllowEmptyStrings = false)]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được để trống", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu mới và Mật khẩu kiểm tra không trùng")]
        [Required(ErrorMessage = "Mật khẩu kiểm tra không được để trống", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string MatKhauKiemTra { get; set; }
    }
}