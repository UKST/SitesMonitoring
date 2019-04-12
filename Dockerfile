FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SitesMonitoring.API/SitesMonitoring.API.csproj", "SitesMonitoring.API/"]
RUN dotnet restore "SitesMonitoring.API/SitesMonitoring.API.csproj"
COPY . .
WORKDIR "/src/SitesMonitoring.API"
RUN dotnet build "SitesMonitoring.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SitesMonitoring.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SitesMonitoring.API.dll"]