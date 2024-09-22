# Usar imagem do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código e fazer build
COPY . .
RUN dotnet publish -c Release -o out

# Usar imagem do runtime para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Sales.API.dll"]