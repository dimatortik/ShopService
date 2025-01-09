using MediatR;
using Microsoft.Extensions.Logging;
using ShopService.Application.Messaging;
using ShopService.Infrastructure.Caching;
using ShopService.Shared;

namespace ShopService.Application.Behaviors;

public class QueryCachingBehavior<TRequest, TResponse>(ICachingService cache, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICachedQuery
    where TResponse : Result

{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await cache
            .GetAsync<TResponse>(request.CacheKey);
        
        string requestName = request.GetType().Name;

        if (cachedResult is not null)
        {
            logger.LogInformation($"Cache hit for key {requestName}");
            return cachedResult;
        }
        
        logger.LogInformation($"Cache miss for key {requestName}");
        TResponse response = await next();
        if (response.IsSuccess)
        {
            await cache.SetAsync(request.CacheKey, response, request.Expiration);
        }

        return response;

    }
}