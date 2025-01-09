using MediatR;
using ShopService.Shared;

namespace ShopService.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    
}