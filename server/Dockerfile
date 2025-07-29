FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./server/TaskOverflow.sln .
COPY ./server/TaskOverflow.Api/TaskOverflow.Api.csproj ./TaskOverflow.Api/

RUN dotnet restore

WORKDIR /src/TaskOverflow.Api

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "TaskOverflow.Api.dll"]