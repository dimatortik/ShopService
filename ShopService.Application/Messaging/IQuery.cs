using MediatR;
using ShopService.Shared;

namespace ShopService.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
} 