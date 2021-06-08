#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Advertiser.WebApi.Hosting/Advertiser.WebApi.Hosting.csproj", "src/Advertiser.WebApi.Hosting/"]
COPY ["src/Advertiser.Application/Advertiser.Application.csproj", "src/Advertiser.Application/"]
COPY ["src/Advertiser.Domain/Advertiser.Domain.csproj", "src/Advertiser.Domain/"]
COPY ["src/Advertiser.WebApi/Advertiser.WebApi.csproj", "src/Advertiser.WebApi/"]
RUN dotnet restore "src/Advertiser.WebApi.Hosting/Advertiser.WebApi.Hosting.csproj"
COPY . .
WORKDIR "/src/src/Advertiser.WebApi.Hosting"
RUN dotnet build "Advertiser.WebApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advertiser.WebApi.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advertiser.WebApi.Hosting.dll"]