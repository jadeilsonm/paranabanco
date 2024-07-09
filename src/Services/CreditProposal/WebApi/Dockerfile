FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8083
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY src/Services/CreditProposal/WebAPI ./WebApi
COPY src/Services/CreditProposal/Core ./Core
COPY src/Services/CreditProposal/Aplication ./Aplication
COPY src/Services/CreditProposal/Infrastructure ./Infrastructure
RUN dotnet restore "WebApi/WebAPI.csproj"
WORKDIR "/src/WebApi"

RUN dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./WebAPI.csproj" -c Release -o /app/publish
ARG BUILD_CONFIGURATION=Release

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
