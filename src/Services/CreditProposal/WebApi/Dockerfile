FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8083
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY src/Services/CreditProposal/WebApi ./WebApi
COPY src/Services/CreditProposal/Core ./Core
COPY src/Services/CreditProposal/Application ./Application
COPY src/Services/CreditProposal/Infrastructure ./Infrastructure
RUN dotnet restore "WebApi/WebApi.csproj"
WORKDIR "/src/WebApi"

RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApi.csproj" -c Release -o /app/publish
ARG BUILD_CONFIGURATION=Release

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
