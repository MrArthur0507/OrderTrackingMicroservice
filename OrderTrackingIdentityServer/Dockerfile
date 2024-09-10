
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY OrderTrackingIdentityServer/OrderTrackingIdentityServer.csproj ./OrderTrackingIdentityServer/
RUN dotnet restore OrderTrackingIdentityServer/OrderTrackingIdentityServer.csproj


COPY OrderTrackingIdentityServer/ ./OrderTrackingIdentityServer/

RUN dotnet publish OrderTrackingIdentityServer/OrderTrackingIdentityServer.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


COPY --from=build /app/out .


EXPOSE 5000


ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "OrderTrackingIdentityServer.dll"]
