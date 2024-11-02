FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /API
COPY . .
RUN dotnet restore "EldoMvideoAPI.csproj"
RUN dotnet build "EldoMvideoAPI.csproj" -c Debug -o /app/build

RUN dotnet publish "EldoMvideoAPI.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EldoMvideoAPI.dll"]