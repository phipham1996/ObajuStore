using ObajuStore.Model.Abstracts;
using System.Collections.Generic;

namespace ObajuStore.Web.Models
{
    public class TagViewModel : Auditable
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<PostTagViewModel> PostTags { get; set; }
    }
}