﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebHungdongho.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LienHe
    {
        [Key]
        [Display(Name = "Mã Liên Hệ")]
        public int MaLienHe { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Nội Dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; }

        [Display(Name = "Ngày Liên Hệ")]
        [Required(ErrorMessage = "Ngày liên hệ không được để trống")]
        public Nullable<System.DateTime> NgayLienHe { get; set; }
    }
}