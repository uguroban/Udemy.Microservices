FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentityServer/Udemy.IdentityServer/Udemy.IdentityServer.csproj", "IdentityServer/Udemy.IdentityServer/"]
RUN dotnet restore "IdentityServer/Udemy.IdentityServer/Udemy.IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/Udemy.IdentityServer"
RUN dotnet build "Udemy.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udemy.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udemy.IdentityServer.dll"]
