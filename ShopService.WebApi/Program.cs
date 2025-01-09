using Microsoft.EntityFrameworkCore;
using ShopService.Application;
using ShopService.Application.Behaviors;
using ShopService.Infrastructure.Caching;
using ShopService.Infrastructure.Data;
using ShopService.Infrastructure.Data.DbInitializer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var connectionString = configuration.GetConnectionString("ShopDb"); ;
var redisCon = configuration.GetConnectionString("Redis") ;

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
    cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
});
services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = redisCon;
});
services.AddScoped<ICachingService, CachingService>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
SeedDatabase();

app.Run();

void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}