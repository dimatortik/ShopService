using Microsoft.EntityFrameworkCore;
using ShopService.Application.Messaging;
using ShopService.Infrastructure.Data;
using ShopService.Shared;

namespace ShopService.Application.GetPopularCategories;

public class GetPopularCategoriesQueryHandler(ShopDbContext context) : IQueryHandler<GetPopularCategoriesQuery, PagedList<PopularCategoryResponse>>
{
    public async Task<Result<PagedList<PopularCategoryResponse>>> Handle(
        GetPopularCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var isCustomerExists = await context.Customers
            .AsNoTracking()
            .AnyAsync(c => c.Id == request.CustomerId, cancellationToken: cancellationToken);

        if (!isCustomerExists)
        {
            return Result.Failure<PagedList<PopularCategoryResponse>>(
                new Error("validation.error","Customer not found"));
        }
        
        var query = context.Customers
            .AsNoTracking()
            .Where(c => c.Id == request.CustomerId)
            .SelectMany(c => c.Purchases)
            .SelectMany(p => p.Items)
            .GroupBy(i => i.Product.Category)
            .Select(g => new PopularCategoryResponse(
                g.Key,
                g.Sum(i => i.Quantity)
            ));
        
//         var query = context.Database
//             .SqlQuery<PopularCategoryResponse>(
//                 $"""
//                  SELECT 
//                    pr."Category" AS "CategoryName",
//                    SUM(pi."Quantity") AS "BoughtProductsCount"
//                  FROM public."Customers" c
//                  JOIN public."Purchases" p ON c."Id" = p."CustomerId"
//                  JOIN public."PurchaseItems" pi ON p."Id" = pi."PurchaseId"
//                  JOIN public."Products" pr ON pi."ProductId" = pr."Id"
//                  WHERE c."Id" = {request.CustomerId}
//                  GROUP BY pr."Category"
//                  """)
//             .AsNoTracking();
        
        var result = await PagedList<PopularCategoryResponse>
            .CreateAsync(query, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return result;
        }
        
        return Result.Success(result.Value!);
    }
}