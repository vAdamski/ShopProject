FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DuendeIdentityServer/DuendeIdentityServer.csproj", "DuendeIdentityServer/"]
RUN dotnet restore "DuendeIdentityServer/DuendeIdentityServer.csproj"
COPY . .
WORKDIR "/src/DuendeIdentityServer"
RUN dotnet build "DuendeIdentityServer.csproj" -c Release -o /app/build
RUN dotnet publish "DuendeIdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DuendeIdentityServer.dll"]