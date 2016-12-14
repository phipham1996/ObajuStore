namespace ObajuStore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ObajuStoreDbContext dbContext;

        public ObajuStoreDbContext Init()
        {
            return dbContext ?? (dbContext = new ObajuStoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}