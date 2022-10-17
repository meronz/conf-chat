# syntax=docker/dockerfile:1.4
ARG BUILD_CONFIG=Release
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIG

COPY src/Client/*.csproj   /src/Client/
COPY src/Server/*.csproj   /src/Server/
COPY src/Shared/*.csproj   /src/Shared/
COPY *.sln                 /
RUN dotnet restore

COPY src/Client /src/Client/
COPY src/Server /src/Server/
COPY src/Shared /src/Shared/
WORKDIR /src/Server/
RUN dotnet publish --no-restore -c ${BUILD_CONFIG} -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
ARG BUILD_CONFIG
WORKDIR /app

COPY --from=build /app /app
ENTRYPOINT ["dotnet", "ConfChat.Server.dll"]
EXPOSE 8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS='http://+:8080'
ENV ASPNETCORE_ENVIRONMENT='Production'
ENV Logging__Console__FormatterName='Simple'
