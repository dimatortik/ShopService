using ShopService.Application.Messaging;

namespace ShopService.Application.GetPopularCategories;

public record GetPopularCategoriesQuery(Guid CustomerId, int PageNumber, int PageSize) : IQuery<PagedList<PopularCategoryResponse>>, ICachedQuery
{
    public string CacheKey { get; } = $"{CustomerId}-PopularCategories";
    public TimeSpan? Expiration { get; } = TimeSpan.FromHours(1);
}

public record PopularCategoryResponse(string CategoryName, int BoughtProductsCount);
