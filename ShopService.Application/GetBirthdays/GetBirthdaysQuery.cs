using ShopService.Application.Messaging;

namespace ShopService.Application.GetBirthdays;

public record GetBirthdaysQuery(
    DateOnly BirthDate,
    int PageNumber,
    int PageSize) : IQuery<PagedList<CustomerResponse>>;


public record CustomerResponse(Guid Id, string FullName);
