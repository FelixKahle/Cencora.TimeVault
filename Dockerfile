# Copyright © 2024 Cencora. All rights reserved.
#
# Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cencora.TimeVault.WebApi/Cencora.TimeVault.WebApi.csproj", "Cencora.TimeVault.WebApi/"]
RUN dotnet restore "Cencora.TimeVault.WebApi/Cencora.TimeVault.WebApi.csproj"
COPY . .
WORKDIR "/src/Cencora.TimeVault.WebApi"
RUN dotnet build "Cencora.TimeVault.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cencora.TimeVault.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cencora.TimeVault.WebApi.dll"]
