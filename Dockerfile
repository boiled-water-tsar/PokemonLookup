FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY PokeLookup/*.csproj ./PokeLookup/
RUN dotnet restore ./PokeLookup/*.csproj

# copy everything else and build app
COPY PokeLookup/. ./PokeLookup/
WORKDIR /source/PokeLookup
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "PokeLookup.dll"]
