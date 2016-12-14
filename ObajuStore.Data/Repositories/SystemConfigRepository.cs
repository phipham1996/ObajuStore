using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig> { }

    public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}