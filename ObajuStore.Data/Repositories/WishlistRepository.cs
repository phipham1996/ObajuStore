using ObajuStore.Data.Infrastructure;
using ObajuStore.Model.Models;

namespace ObajuStore.Data.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
    }

    public class WishlistRepository : RepositoryBase<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}