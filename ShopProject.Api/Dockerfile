FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShopProject.Api/ShopProject.Api.csproj", "ShopProject.Api/"]
COPY ["ShopProject.Shared/ShopProject.Shared.csproj", "ShopProject.Shared/"]
COPY ["ShopProject.Application/ShopProject.Application.csproj", "ShopProject.Application/"]
COPY ["ShopProject.Infrastructure/ShopProject.Infrastructure.csproj", "ShopProject.Infrastructure/"]
COPY ["ShopProject.Domain/ShopProject.Domain.csproj", "ShopProject.Domain/"]
COPY ["ShopProject.Persistence/ShopProject.Persistence.csproj", "ShopProject.Persistence/"]
RUN dotnet restore "ShopProject.Api/ShopProject.Api.csproj"
COPY . .
WORKDIR "/src/ShopProject.Api"
RUN dotnet build "ShopProject.Api.csproj" -c Release -o /app/build
RUN dotnet publish "ShopProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopProject.Api.dll"]
