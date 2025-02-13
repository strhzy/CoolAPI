# Базовый образ с ASP.NET для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Образ для сборки с .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EldoMvideoAPI/EldoMvideoAPI.csproj", "EldoMvideoAPI/"]
RUN dotnet restore "EldoMvideoAPI/EldoMvideoAPI.csproj"
COPY . . 
WORKDIR "/src/EldoMvideoAPI"
RUN dotnet build "EldoMvideoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации приложения
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EldoMvideoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Базовый финальный образ для запуска
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EldoMvideoAPI.dll"]
