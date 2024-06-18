namespace ProductAdmin.Application.DomainServices.Repositories
{
    public interface ICacheRepository
    {
        TEntity GetMemoryValue<TEntity>(string key);

        void SetMemoryValue<TEntity>(string key, TEntity entity);
    }
}