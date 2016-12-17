using System.ComponentModel.DataAnnotations;
using uStora.Web.Models;

namespace ObajuStore.Web.Models
{
    public class OrderDetailViewModel
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public long ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual OrderViewModel Order { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}