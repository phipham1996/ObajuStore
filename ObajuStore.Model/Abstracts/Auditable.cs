using System;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Model.Abstracts
{
    public abstract class Auditable : IAuditable
    {
        [Display(Name = "Ngày khởi tạo")]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Khởi tạo bởi")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Cập nhật bởi")]
        public string UpdatedBy { get; set; }

        [MaxLength(150)]
        [Display(Name = "Meta Keyword")]
        public string MetaKeyword { get; set; }

        [MaxLength(150)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}