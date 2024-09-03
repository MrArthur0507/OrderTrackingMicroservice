
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["OrderTrackingItemCatalogService/OrderTrackingItemCatalogService.csproj", "OrderTrackingItemCatalogService/"]
COPY ["OrderTrackingItemCatalogService.Models/OrderTrackingItemCatalogService.Models.csproj", "OrderTrackingItemCatalogService.Models/"]


RUN dotnet restore "OrderTrackingItemCatalogService/OrderTrackingItemCatalogService.csproj"


COPY . .


WORKDIR "/src/OrderTrackingItemCatalogService"


RUN dotnet publish "OrderTrackingItemCatalogService.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .


EXPOSE 80


ENTRYPOINT ["dotnet", "OrderTrackingItemCatalogService.dll"]
