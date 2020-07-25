FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY SitesMonitoring.API/SitesMonitoring.API.csproj SitesMonitoring.API/
COPY SitesMonitoring.BLL/SitesMonitoring.BLL.csproj SitesMonitoring.BLL/
COPY SitesMonitoring.DAL/SitesMonitoring.DAL.csproj SitesMonitoring.DAL/
RUN dotnet restore "SitesMonitoring.API/SitesMonitoring.API.csproj"
COPY SitesMonitoring.API/ SitesMonitoring.API/
COPY SitesMonitoring.BLL/ SitesMonitoring.BLL/
COPY SitesMonitoring.DAL/ SitesMonitoring.DAL/
WORKDIR "/src/SitesMonitoring.API"
RUN dotnet build "SitesMonitoring.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SitesMonitoring.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SitesMonitoring.API.dll"]