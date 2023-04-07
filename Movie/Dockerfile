# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app

COPY source/Movie/Movie/MoviesApi/*.csproj ./
RUN dotnet restore

COPY source/Movie/Movie/MoviesApi/ ./
RUN dotnet publish -c Release -o out

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "MoviesApi.dll"]
