
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY OrderTrackingItemCatalogService/OrderTrackingItemCatalogService.csproj OrderTrackingItemCatalogService/
COPY OrderTrackingItemCatalogService.Models/OrderTrackingItemCatalogService.Models.csproj OrderTrackingItemCatalogService.Models/
COPY MassTransitContracts/MassTransitContracts.csproj MassTransitContracts/


RUN dotnet restore OrderTrackingItemCatalogService/OrderTrackingItemCatalogService.csproj


COPY OrderTrackingItemCatalogService/ OrderTrackingItemCatalogService/
COPY OrderTrackingItemCatalogService.Models/ OrderTrackingItemCatalogService.Models/
COPY MassTransitContracts/ MassTransitContracts/

RUN dotnet publish OrderTrackingItemCatalogService/OrderTrackingItemCatalogService.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


COPY --from=build /app/out .


EXPOSE 5001
ENV ASPNETCORE_URLS=http://*:5001

ENTRYPOINT ["dotnet", "OrderTrackingItemCatalogService.dll"]
