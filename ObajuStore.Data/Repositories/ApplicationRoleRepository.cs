using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}