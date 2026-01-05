# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src
EXPOSE 8080
EXPOSE 443

COPY ["TrailerPark.API/TrailerPark.API.csproj", "TrailerPark.API/"]
COPY ["TrailerPark.Core/TrailerPark.Core.csproj", "TrailerPark.Core/"]
COPY ["TrailerPark.Infrastructure/TrailerPark.Infrastructure.csproj", "TrailerPark.Infrastructure/"]
COPY ["TrailerPark.Application/TrailerPark.Application.csproj", "TrailerPark.Application/"]
RUN dotnet restore "TrailerPark.API/TrailerPark.API.csproj" 
RUN dotnet restore "TrailerPark.Core/TrailerPark.Core.csproj" 
RUN dotnet restore "TrailerPark.Infrastructure/TrailerPark.Infrastructure.csproj"
RUN dotnet restore "TrailerPark.Application/TrailerPark.Application.csproj"

COPY . ./
RUN dotnet publish -c Release -o /out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=base /out .
ENTRYPOINT ["dotnet","TrailerPark.API.dll"]
