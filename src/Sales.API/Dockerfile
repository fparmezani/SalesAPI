#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Sales.API/Sales.API.csproj", "src/Sales.API/"]
COPY ["src/Sales.Application/Sales.Application.csproj", "src/Sales.Application/"]
COPY ["src/Sales.Data/Sales.Data.csproj", "src/Sales.Data/"]
COPY ["src/Sales.Domain/Sales.Domain.csproj", "src/Sales.Domain/"]
RUN dotnet restore "./src/Sales.API/Sales.API.csproj"
COPY . .
WORKDIR "/src/src/Sales.API"
RUN dotnet build "./Sales.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sales.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.API.dll"]