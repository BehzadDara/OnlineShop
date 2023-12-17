#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiGateways/OnlineShop.Aggregator/OnlineShop.Aggregator/OnlineShop.Aggregator.csproj", "ApiGateways/OnlineShop.Aggregator/OnlineShop.Aggregator/"]
RUN dotnet restore "./ApiGateways/OnlineShop.Aggregator/OnlineShop.Aggregator/./OnlineShop.Aggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/OnlineShop.Aggregator/OnlineShop.Aggregator"
RUN dotnet build "./OnlineShop.Aggregator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShop.Aggregator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.Aggregator.dll"]