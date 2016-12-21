using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Bạn cần nhập họ tên")]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string FullName { set; get; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Bạn cần nhập ngày sinh")]
        public DateTime BirthDay { set; get; }

        [Display(Name = "Giới thiệu bản thân")]
        public string Bio { set; get; }

        [EmailAddress]
        [Required(ErrorMessage = "Bạn cần nhập Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }

        [Display(Name = "Giới tính")]
        public string Gender { set; get; }

        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Display(Name = "Đã xóa")]
        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { set; get; }

    }
}