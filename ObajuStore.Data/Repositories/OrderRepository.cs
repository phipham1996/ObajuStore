using ObajuStore.Common.ViewModels;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ObajuStore.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<OrderClientViewModel> GetListOrder(string userId);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<OrderClientViewModel> GetListOrder(string userId)
        {
            var parameter = new SqlParameter("@UserId", userId);
            return DbContext.Database.SqlQuery<OrderClientViewModel>("ListShoppingCart @UserId", parameter);
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenuesStatistic  @fromDate,@toDate", parameters);
        }
    }
}