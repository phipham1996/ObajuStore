namespace ObajuStore.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}