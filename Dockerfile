# Imagem base para o ASP.NET Core (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usando a imagem do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Oasis API.csproj", "./"]
COPY ["OasisApi.Domain/OasisApi.Domain.csproj", "OasisApi.Domain/"]
COPY ["OasisApi.Application/OasisApi.Application.csproj", "OasisApi.Application/"]
COPY ["OasisApi.Infrastructure/OasisApi.Infrastructure.csproj", "OasisApi.Infrastructure/"]
RUN dotnet restore "Oasis API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Oasis API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oasis API.csproj" -c Release -o /app/publish

# Definindo o container final com o .NET Runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oasis API.dll"]
