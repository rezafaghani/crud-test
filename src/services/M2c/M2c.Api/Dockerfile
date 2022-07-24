FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/services/M2c/M2c.Api/M2c.Api.csproj", "src/services/M2c/M2c.Api/"]
COPY ["src/services/M2c/M2c.Domain/M2c.Domain.csproj", "src/services/M2c/M2c.Domain/"]
COPY ["src/services/M2c/M2c.Infrastructure/M2c.Infrastructure.csproj", "src/services/M2c/M2c.Infrastructure/"]
RUN dotnet restore "src/services/M2c/M2c.Api/M2c.Api.csproj"
COPY . .
WORKDIR "src/services/M2c/M2c.Api"
RUN dotnet build "M2c.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "M2c.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "M2c.Api.dll"]
