using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Mã bảo mật")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Gi nhớ trên trình duyệt này?")]
        public bool RememberBrowser { get; set; }
        [Display(Name = "Ghi nhớ")]
        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Vui lòng nhập đúng định dạng Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Vui lòng nhập đúng định dạng Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ tên")]
        [MaxLength(50, ErrorMessage = "Họ tên không quá 50 ký tự.")]
        [Display(Name = "Họ tên")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không quá 50 ký tự.")]
        public string Username { get; set; }

        [MaxLength(256, ErrorMessage = "Mật khẩu không quá 256 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "Chỉ nhập 20 ký tự")]
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Bạn chưa chọn giới tính")]
        public string Gender { get; set; }

        [MaxLength(ErrorMessage = "Chỉ nhập 30 chữ số")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
