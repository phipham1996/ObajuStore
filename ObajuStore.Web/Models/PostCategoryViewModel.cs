using ObajuStore.Model.Abstracts;

namespace ObajuStore.Web.Models
{
    public class PostCategoryViewModel : Auditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }
    }
}