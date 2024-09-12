FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY OrderTrackingOrderingService/OrderTrackingOrderingService.csproj OrderTrackingOrderingService/
COPY OrderTrackingOrderingService.Models/OrderTrackingOrderingService.Models.csproj OrderTrackingOrderingService.Models/
COPY OrderTrackingOrderingService.DataAccess/OrderTrackingOrderingService.DataAccess.csproj OrderTrackingOrderingService.DataAccess/
COPY MassTransitContracts/MassTransitContracts.csproj MassTransitContracts/


RUN dotnet restore OrderTrackingOrderingService/OrderTrackingOrderingService.csproj


COPY OrderTrackingOrderingService/ OrderTrackingOrderingService/
COPY OrderTrackingOrderingService.Models/ OrderTrackingOrderingService.Models/
COPY OrderTrackingOrderingService.DataAccess/ OrderTrackingOrderingService.DataAccess/
COPY MassTransitContracts/ MassTransitContracts/

RUN dotnet publish OrderTrackingOrderingService/OrderTrackingOrderingService.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


COPY --from=build /app/out .


EXPOSE 5002
ENV ASPNETCORE_URLS=http://*:5002
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "OrderTrackingOrderingService.dll"]