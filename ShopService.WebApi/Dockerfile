FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShopService.WebApi/ShopService.WebApi.csproj", "ShopService.WebApi/"]
COPY ["ShopService.Application/ShopService.Application.csproj", "ShopService.Application/"]
COPY ["ShopService.Shared/ShopService.Shared.csproj", "ShopService.Shared/"]
COPY ["ShopService.Infrastructure/ShopService.Infrastructure.csproj", "ShopService.Infrastructure/"]
COPY ["ShopService.Domain/ShopService.Domain.csproj", "ShopService.Domain/"]
RUN dotnet restore "ShopService.WebApi/ShopService.WebApi.csproj"
COPY . .
WORKDIR "/src/ShopService.WebApi"
RUN dotnet build "ShopService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShopService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopService.WebApi.dll"]
