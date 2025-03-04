FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TalentHub.HandshakeService.Api/TalentHub.HandshakeService.Api.csproj", "TalentHub.HandshakeService.Api/"]
COPY ["TalentHub.HandshakeService.Application/TalentHub.HandshakeService.Application.csproj", "TalentHub.HandshakeService.Application/"]
COPY ["TalentHub.HandshakeService.Infrastructure/TalentHub.HandshakeService.Infrastructure.csproj", "TalentHub.HandshakeService.Infrastructure/"]

RUN dotnet restore "TalentHub.HandshakeService.Api/TalentHub.HandshakeService.Api.csproj"
COPY . .
WORKDIR "/src/TalentHub.HandshakeService.Api"
RUN dotnet build "TalentHub.HandshakeService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TalentHub.HandshakeService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentHub.HandshakeService.Api.dll"]