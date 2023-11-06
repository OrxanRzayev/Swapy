FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Swapy.API/Swapy.API.csproj", "Swapy.API/"]
COPY ["Swapy.DAL/Swapy.DAL.csproj", "Swapy.DAL/"]
COPY ["Swapy.BLL/Swapy.BLL.csproj", "Swapy.BLL/"]
COPY ["Swapy.Common/Swapy.Common.csproj", "Swapy.Common/"]
RUN dotnet restore "Swapy.API/Swapy.API.csproj"
RUN dotnet restore "Swapy.BLL/Swapy.BLL.csproj"
RUN dotnet restore "Swapy.DAL/Swapy.DAL.csproj"
RUN dotnet restore "Swapy.Common/Swapy.Common.csproj"
COPY . .
WORKDIR "/src/Swapy.API"
RUN dotnet build "Swapy.API.csproj" -c Release -o /app/build

WORKDIR "/src/Swapy.BLL"
RUN dotnet build "Swapy.BLL.csproj" -c Release -o /app/build

WORKDIR "/src/Swapy.DAL"
RUN dotnet build "Swapy.DAL.csproj" -c Release -o /app/build

WORKDIR "/src/Swapy.Common"
RUN dotnet build "Swapy.Common.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Swapy.API"
RUN dotnet publish "Swapy.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/mssql/server:2019-latest AS db
WORKDIR /usr/work
COPY ["Swapy.DAL/Database/*.sql", "."]
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=1Secure*Password1

RUN (/opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i DockerDbInit.sql \
    && pkill sqlservr

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Swapy.API.dll"]