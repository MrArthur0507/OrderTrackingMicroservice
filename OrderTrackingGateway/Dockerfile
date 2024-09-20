FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY OrderTrackingGateway/OrderTrackingGateway.csproj ./OrderTrackingGateway/
RUN dotnet restore OrderTrackingGateway/OrderTrackingGateway.csproj


COPY OrderTrackingGateway/ ./OrderTrackingGateway/

RUN dotnet publish OrderTrackingGateway/OrderTrackingGateway.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


COPY --from=build /app/out .


EXPOSE 7000


ENV ASPNETCORE_URLS=http://*:7000
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "OrderTrackingGateway.dll"]