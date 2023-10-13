FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShopProject.Api/ShopProject.Api.csproj", "ShopProject.Api/"]
RUN dotnet restore "ShopProject.Api/ShopProject.Api.csproj"
COPY . .
WORKDIR "/src/ShopProject.Api"
RUN dotnet build "ShopProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopProject.Api.dll"]
