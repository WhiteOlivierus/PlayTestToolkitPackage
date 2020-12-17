FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PlayTestBuildsAPI/PlayTestBuildsAPI.csproj", "PlayTestBuildsAPI/"]
RUN dotnet restore "PlayTestBuildsAPI/PlayTestBuildsAPI.csproj"
COPY . .
WORKDIR "/src/PlayTestBuildsAPI"
RUN dotnet build "PlayTestBuildsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayTestBuildsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayTestBuildsAPI.dll"]