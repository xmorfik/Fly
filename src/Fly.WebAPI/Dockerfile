#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Fly.WebAPI/Fly.WebAPI.csproj", "Fly.WebAPI/"]
RUN dotnet restore "Fly.WebAPI/Fly.WebAPI.csproj"
COPY . .
WORKDIR "/src/Fly.WebAPI"
RUN dotnet build "Fly.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fly.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fly.WebAPI.dll"]