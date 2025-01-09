using Microsoft.EntityFrameworkCore;
using ShopService.Application.Messaging;
using ShopService.Infrastructure.Data;
using ShopService.Shared;

namespace ShopService.Application.GetLastCustomersByPeriod;

public class GetLastCustomersByPeriodQueryHandler(ShopDbContext context): IQueryHandler<GetLastCustomersByPeriodQuery, PagedList<CustomerWithPurchaseDayResponse>>
{
    public async Task<Result<PagedList<CustomerWithPurchaseDayResponse>>> Handle(
        GetLastCustomersByPeriodQuery request, 
        CancellationToken cancellationToken)
    {
        var customersQuery = context.Customers
            .AsNoTracking()
            .Where(c => c.CreatedAt >= DateTime.UtcNow.AddDays(-request.Days))
            .Select(c => new CustomerWithPurchaseDayResponse(
                c.Id,
                c.FullName.ToString(),
                c.CreatedAt));
        
        var result = await PagedList<CustomerWithPurchaseDayResponse>
            .CreateAsync(customersQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return result;
        }
        
        return Result.Success(result.Value!);
    }
}