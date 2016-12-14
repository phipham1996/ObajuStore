using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface IPageRepository : IRepository<Page> { }

    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}