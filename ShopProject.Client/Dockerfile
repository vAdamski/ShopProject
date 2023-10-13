FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShopProject.Client/ShopProject.Client.csproj", "ShopProject.Client/"]
RUN dotnet restore "ShopProject.Client/ShopProject.Client.csproj"
COPY . .
WORKDIR "/src/ShopProject.Client"
RUN dotnet build "ShopProject.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopProject.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopProject.Client.dll"]
