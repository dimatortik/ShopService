using ShopService.Application.Messaging;

namespace ShopService.Application.GetLastCustomersByPeriod;

public record GetLastCustomersByPeriodQuery(int Days, int PageNumber, int PageSize) : IQuery<PagedList<CustomerWithPurchaseDayResponse>>;


public record CustomerWithPurchaseDayResponse(Guid Id, string FullName, DateTime PurchaseDay);
