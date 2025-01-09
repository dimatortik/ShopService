using MediatR;
using ShopService.Shared;

namespace ShopService.Application.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}