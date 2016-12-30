using ObajuStore.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObajuStore.Web.Models
{
    public class ProductCategoryViewModel : Auditable
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tiêu đề SEO")]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Alias { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(700, ErrorMessage = "Chỉ nhập 700 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Bạn phải nhập số")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool HomeFlag { get; set; }

        [Display(Name = "Danh mục cha")]
        public IList<SelectListItem> ParentList { get; set; }
    }
}