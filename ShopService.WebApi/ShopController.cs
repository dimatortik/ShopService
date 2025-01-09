using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.GetBirthdays;
using ShopService.Application.GetLastCustomersByPeriod;
using ShopService.Application.GetPopularCategories;

namespace ShopService.WebApi;

public class ShopController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    [Route("api/popular-categories/{customerId}")]
    public async Task<IActionResult> GetPopularCategories([FromRoute] Guid customerId, int pageNumber, int pageSize)
    {
        var query = new GetPopularCategoriesQuery(customerId, pageNumber, pageSize);
        var result = await sender.Send(query);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpGet]
    [Route("api/last-customers")]
    public async Task<IActionResult> GetLastCustomers(int days, int pageNumber, int pageSize)
    {
        var query = new GetLastCustomersByPeriodQuery(days, pageNumber, pageSize);
        var result = await sender.Send(query);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);;
    }
    
    [HttpGet]
    [Route("api/get-birthdays")]
    public async Task<IActionResult> GetBirthdays(DateOnly date, int pageNumber, int pageSize)
    {
        var query = new GetBirthdaysQuery(date, pageNumber, pageSize);
        var result = await sender.Send(query);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);;
    }
    
}