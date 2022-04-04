#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/FleetManagement.Api/FleetManagement.Api.csproj", "src/FleetManagement.Api/"]
COPY ["src/FleetManagement.Infrastructure/FleetManagement.Infrastructure.csproj", "src/FleetManagement.Infrastructure/"]
COPY ["src/FleetManagement.Common/FleetManagement.Common.csproj", "src/FleetManagement.Common/"]
COPY ["src/FleetManagement.Application/FleetManagement.Application.csproj", "src/FleetManagement.Application/"]
RUN dotnet restore "src/FleetManagement.Api/FleetManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/FleetManagement.Api"
RUN dotnet build "FleetManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetManagement.Api.dll"]