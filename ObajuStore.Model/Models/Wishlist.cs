using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ObajuStore.Model.Abstracts;

namespace ObajuStore.Model.Models
{
    [Table("Wishlists")]
    public class Wishlist : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string UserId { get; set; }

        public long ProductId { get; set; }

        public bool? IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}