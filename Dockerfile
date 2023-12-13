FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["UtmShop.Blazor/UtmShop.Blazor.csproj", "UtmShop.Blazor/"]
COPY ["UtmShop.Models/UtmShop.Models.csproj", "UtmShop.Models/"]
RUN dotnet restore "UtmShop.Blazor/UtmShop.Blazor.csproj"
COPY . .
WORKDIR "/src/UtmShop.Blazor"
RUN dotnet build "UtmShop.Blazor.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "UtmShop.Blazor.csproj" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UtmShop.Blazor.dll"]