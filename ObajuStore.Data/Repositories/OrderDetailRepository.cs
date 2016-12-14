using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail> { }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}