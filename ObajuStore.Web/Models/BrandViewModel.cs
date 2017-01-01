using ObajuStore.Model.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Web.Models
{
    public class BrandViewModel : Auditable
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên thương hiệu")]
        [Display(Name = "Tên thương hiệu")]
        [StringLength(150, ErrorMessage = "Chỉ nhập 150 ký tự")]
        public string Name { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Quốc gia")]
        [StringLength(150, ErrorMessage = "Chỉ nhập 150 ký tự")]
        public string Country { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(750, ErrorMessage = "Chỉ nhập 750 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Image { get; set; }

        [Url]
        [StringLength(100, ErrorMessage = "Chỉ nhập 100 ký tự")]
        public string Website { get; set; }

        public bool HotFlag { get; set; }
    }
}