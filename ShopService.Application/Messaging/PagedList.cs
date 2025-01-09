using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using ShopService.Shared;

namespace ShopService.Application.Messaging;

public class PagedList<T>
{
    private PagedList() { }
    [JsonConstructor]
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public List<T> Items { get; set; } 
    public int Page { get; set; } 
    public int PageSize { get; set; } 
    public int TotalCount { get; set; } 
    public bool HasNext => Page * PageSize < TotalCount;
    public bool HasPrevious => Page > 1;

    public static async Task<Result<PagedList<T>>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var count = await query.CountAsync();
        try
        {
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedList<T>(items, page, pageSize, count);
        }
        catch (Exception e)
        {
            return Result.Failure<PagedList<T>>(new Error(
                "get_paged_list",
                "Error while creating paged list."));
        }
        
    }
    
}