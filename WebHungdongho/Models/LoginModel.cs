using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebHungdongho.Models
{
    public class LoginModel
    {
        [Key]
        [DisplayName("Tên đăng nhập")]
         [Required(ErrorMessage = "Tên đăng nhập không được trống.")]
         public string UserName { get; set; }
          [DisplayName("Mật khẩu")]

        [Required(ErrorMessage = "Mật khẩu không được trống.")]
         [DataType(DataType.Password)]
            public string Password { get; set; }
    }
}