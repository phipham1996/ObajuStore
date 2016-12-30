using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ObajuStore.Model.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }
        public bool? IsDeleted { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}