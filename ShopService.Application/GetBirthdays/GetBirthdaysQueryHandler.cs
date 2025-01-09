using Microsoft.EntityFrameworkCore;
using ShopService.Application.Messaging;
using ShopService.Infrastructure.Data;
using ShopService.Shared;

namespace ShopService.Application.GetBirthdays;

public class GetBirthdaysQueryHandler(ShopDbContext context) : IQueryHandler<GetBirthdaysQuery, PagedList<CustomerResponse>>
{
    public async Task<Result<PagedList<CustomerResponse>>> Handle(GetBirthdaysQuery request, CancellationToken cancellationToken)
    {
        var customersQuery = context.Customers
            .AsNoTracking()
            .Where(c => c.BirthDate == request.BirthDate)
            .Select(c => new CustomerResponse(
                c.Id,
                c.FullName.ToString()));
        
        var result = await PagedList<CustomerResponse>
            .CreateAsync(customersQuery, request.PageNumber, request.PageSize);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        return Result.Success(result.Value!);
    }
}