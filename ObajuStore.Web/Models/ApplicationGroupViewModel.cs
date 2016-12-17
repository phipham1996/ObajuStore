using System.Collections.Generic;

namespace ObajuStore.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
}