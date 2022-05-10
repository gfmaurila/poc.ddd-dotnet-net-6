#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["01 - Presentation/Demo.API/Demo.API.csproj", "01 - Presentation/Demo.API/"]
COPY ["04  - Infrastructure/4.2 - CrossCutting/Demo.Infrastruture.CrossCutting.IOC/Demo.Infrastruture.CrossCutting.IOC.csproj", "04  - Infrastructure/4.2 - CrossCutting/Demo.Infrastruture.CrossCutting.IOC/"]
COPY ["03 - Domain/Demo.Domain/Demo.Domain.csproj", "03 - Domain/Demo.Domain/"]
COPY ["04  - Infrastructure/4.2 - CrossCutting/Demo.Infrastruture.CrossCutting.Core/Demo.Infrastruture.CrossCutting/Demo.Infrastruture.CrossCutting.csproj", "04  - Infrastructure/4.2 - CrossCutting/Demo.Infrastruture.CrossCutting.Core/Demo.Infrastruture.CrossCutting/"]
COPY ["03 - Domain/Demo.Domain.Services/Demo.Domain.Services.csproj", "03 - Domain/Demo.Domain.Services/"]
COPY ["03 - Domain/Demo.Domain.Core/Demo.Domain.Core.csproj", "03 - Domain/Demo.Domain.Core/"]
COPY ["02 - Application/Demo.Application.Demo.DTO/Demo.Application.Demo.DTO.csproj", "02 - Application/Demo.Application.Demo.DTO/"]
COPY ["04  - Infrastructure/4.1 - Data/Demo.Infrastruture.Repository/Demo.Infrastruture.Repository.csproj", "04  - Infrastructure/4.1 - Data/Demo.Infrastruture.Repository/"]
COPY ["04  - Infrastructure/4.1 - Data/Demo.Infrastructure.Data/Demo.Infrastructure.Data.csproj", "04  - Infrastructure/4.1 - Data/Demo.Infrastructure.Data/"]
RUN dotnet restore "01 - Presentation/Demo.API/Demo.API.csproj"
COPY . .
WORKDIR "/src/01 - Presentation/Demo.API"
RUN dotnet build "Demo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.API.dll"]