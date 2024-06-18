namespace ProductAdmin.Application.SystemContext.UseCase;

using Microsoft.Extensions.Configuration;
using ProductAdmin.Application.DomainServices.Repositories;
using ProductAdmin.Application.SystemContext.UseCase.Contracts;
using ProductAdmin.Domain.SharedKernel;

public class SetStatusInCacheUseCase(ICacheRepository cacheRepository, IConfiguration configuration) : ISetStatusInCache
{
    private readonly ICacheRepository _cacheRepository = cacheRepository;
    private readonly string _cacheKey = configuration.GetSection("Cache:StatusKey").Value ?? throw new ArgumentNullException(nameof(configuration));

    public List<Status> ExecuteAsync()
    {
        List<Status> status = _cacheRepository.GetMemoryValue<List<Status>>(_cacheKey);

        if (status is null)
        {
            List<Status> list = [Status.BuildActive(), Status.BuildInactive()];
            _cacheRepository.SetMemoryValue(_cacheKey, list);
            status = new(list);
        }

        return status;
    }
}