FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Exchange.Rate.API/Exchange.Rate.API.csproj", "src/Exchange.Rate.API/"]
RUN dotnet restore "src/Exchange.Rate.API/Exchange.Rate.API.csproj"
COPY . .
WORKDIR "/src/src/Exchange.Rate.API"
RUN dotnet build "Exchange.Rate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exchange.Rate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK CMD curl --fail http://localhost:5000/health || exit 1

ENTRYPOINT ["dotnet", "Exchange.Rate.API.dll"]
