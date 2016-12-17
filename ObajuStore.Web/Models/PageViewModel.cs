using ObajuStore.Model.Abstracts;

namespace ObajuStore.Web.Models
{
    public class PageViewModel : Auditable
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Content { set; get; }
    }
}