using MvcPaging;
using System;
using ObajuStore.Model.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObajuStore.Web.Models
{
    [Serializable]
    public class ProductViewModel : Auditable
    {
        public long ID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Name { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryID { get; set; }

        [Display(Name = "Thương hiệu")]
        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int BrandID { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Image { get; set; }

        [Display(Name = "Bộ hình ảnh")]
        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        public decimal Price { get; set; }

        [Display(Name = "Giá gốc")]
        [Required(ErrorMessage = "Vui lòng nhập giá gốc")]
        public decimal OriginalPrice { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Bảo hành")]
        public int? Warranty { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [StringLength(700, ErrorMessage = "Chỉ nhập 700 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Mô tả")]
        public string Content { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool HomeFlag { get; set; }

        [Display(Name = "Sản phẩm hot")]
        public bool HotFlag { get; set; }

        [Display(Name = "Xóa sản phẩm này?")]
        public bool IsDelete { get; set; }

        [Display(Name = "Lượt xem")]
        public long ViewCount { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        public string Tags { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { get; set; }
        public IPagedList<ProductViewModel> ListProducts { get; set; }
        public virtual BrandViewModel Brands { get; set; }

        public IList<SelectListItem> CategoryList { get; set; }
    }
}