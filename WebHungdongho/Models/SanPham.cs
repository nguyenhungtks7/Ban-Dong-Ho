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

    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            this.DanhMucs = new HashSet<DanhMuc>();
        }

        [Key]
        [Display(Name = "Mã Sản Phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSP { get; set; }

        [Display(Name = "Nhãn Hiệu")]
        public string NhanHieu { get; set; }

        [Display(Name = "Mô Tả")]
        public string MoTa { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là một số không âm")]
        public Nullable<decimal> Gia { get; set; }

        [Display(Name = "Số Lượng Kho")]
        [Required(ErrorMessage = "Số lượng kho không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng kho phải là một số không âm")]
        public Nullable<int> SoLuongKho { get; set; }

        [Display(Name = "Hình ảnh")]
        public string DuongDanHinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMuc> DanhMucs { get; set; }
    }
}
