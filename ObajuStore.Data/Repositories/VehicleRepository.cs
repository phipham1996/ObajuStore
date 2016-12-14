using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
    }

    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}