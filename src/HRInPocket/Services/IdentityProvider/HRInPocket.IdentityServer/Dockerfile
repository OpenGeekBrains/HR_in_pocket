#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/IdentityProvider/HRInPocket.IdentityServer/HRInPocket.IdentityServer.csproj", "Services/IdentityProvider/HRInPocket.IdentityServer/"]
RUN dotnet restore "Services/IdentityProvider/HRInPocket.IdentityServer/HRInPocket.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/IdentityProvider/HRInPocket.IdentityServer"
RUN dotnet build "HRInPocket.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRInPocket.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRInPocket.IdentityServer.dll"]