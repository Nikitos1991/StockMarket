FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StockMarket.Api/StockMarket.Api.csproj", "StockMarket.Api/"]
RUN dotnet restore "StockMarket.Api/StockMarket.Api.csproj"
COPY . .
WORKDIR "/src/StockMarket.Api"
RUN dotnet build "StockMarket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockMarket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockMarket.Api.dll"]