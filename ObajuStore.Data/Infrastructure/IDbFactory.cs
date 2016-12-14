using System;

namespace ObajuStore.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ObajuStoreDbContext Init();
    }
}