#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["UI/WEB/HRInPocket/HRInPocket.csproj", "UI/WEB/HRInPocket/"]
COPY ["Services/HRInPocket.Clients/HRInPocket.Clients.csproj", "Services/HRInPocket.Clients/"]
COPY ["Services/HRInPocket.Interfaces/HRInPocket.Interfaces.csproj", "Services/HRInPocket.Interfaces/"]
COPY ["Common/HRInPocket.Domain/HRInPocket.Domain.csproj", "Common/HRInPocket.Domain/"]
COPY ["Services/HRInPocket.DAL/HRInPocket.DAL.csproj", "Services/HRInPocket.DAL/"]
COPY ["Services/HRInPocket.Services/HRInPocket.Services.csproj", "Services/HRInPocket.Services/"]
COPY ["Services/HRInPocket.Extensions/HRInPocket.Extensions.csproj", "Services/HRInPocket.Extensions/"]
RUN dotnet restore "UI/WEB/HRInPocket/HRInPocket.csproj"
COPY . .
WORKDIR "/src/UI/WEB/HRInPocket"
RUN dotnet build "HRInPocket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRInPocket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRInPocket.dll"]
