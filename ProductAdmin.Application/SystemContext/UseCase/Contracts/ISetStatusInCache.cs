using ProductAdmin.Domain.SharedKernel;

namespace ProductAdmin.Application.SystemContext.UseCase.Contracts;

public interface ISetStatusInCache
{
    List<Status> ExecuteAsync();
}