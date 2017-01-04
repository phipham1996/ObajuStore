using System.Collections.Generic;

namespace ObajuStore.Web.Models
{
    public class SidebarViewModel
    {
        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }
        public IEnumerable<BrandViewModel> Brands { get; set; }
    }
}