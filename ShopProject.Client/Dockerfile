FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["ShopProject.Client/ShopProject.Client.csproj", "ShopProject.Client/"]
COPY ["ShopProject.Shared/ShopProject.Shared.csproj", "ShopProject.Shared/"]
RUN dotnet restore "ShopProject.Client/ShopProject.Client.csproj"
COPY . .
RUN dotnet publish "ShopProject.Client/ShopProject.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html