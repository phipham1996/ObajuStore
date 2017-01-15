using System;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Model.Abstracts
{
    public interface IAuditable
    {
        [Display(Name = "Ngày khởi tạo")]
        DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Khởi tạo bởi")]
        string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Cập nhật bởi")]
        string UpdatedBy { get; set; }

        [MaxLength(150)]
        [Display(Name = "Meta Keyword")]
        string MetaKeyword { get; set; }

        [MaxLength(150)]
        [Display(Name = "Meta Description")]
        string MetaDescription { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        bool Status { get; set; }
    }
}