namespace ProductAdmin.Infrastructure.Cache.Memory;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ProductAdmin.Application.DomainServices.Repositories;

public class MemoryCacheService(IMemoryCache memoryCache, IConfiguration configuration) : ICacheRepository
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly int _minutesToExpireCache = configuration.GetValue<int>("Cache:MinutesToExpireCache");

    public TEntity GetMemoryValue<TEntity>(string key)
    {
        if (_memoryCache.TryGetValue(key, out TEntity newValue))
        {
            return newValue;
        }

        return default;
    }

    public void SetMemoryValue<TEntity>(string key, TEntity value)
    {
        _memoryCache.CreateEntry(key);

        MemoryCacheEntryOptions entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_minutesToExpireCache),
            Priority = CacheItemPriority.Low
        };

        _memoryCache.Set(key, value, entryOptions);
    }
}