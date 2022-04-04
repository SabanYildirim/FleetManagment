FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

WORKDIR /src
COPY ["src/FleetManagement.Infrastructure/FleetManagement.Infrastructure.csproj", "src/FleetManagement.Infrastructure/"]
COPY Setup.sh Setup.sh

RUN dotnet tool install --global dotnet-ef --version 5.0.15 

RUN dotnet restore "src/FleetManagement.Infrastructure/FleetManagement.Infrastructure.csproj"
COPY . .
WORKDIR "/src/FleetManagement.Infrastructure"

ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet ef --version migrations add InitialMigrations

RUN chmod +x /src/Setup.sh
CMD /bin/bash /src/Setup.sh