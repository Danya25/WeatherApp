FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY ["WeatherApp/WeatherApp.csproj", "WeatherApp/"]
COPY ["WeatherApp.Common/WeatherApp.Common.csproj", "WeatherApp.Common/"]
COPY ["WeatherApp.Domain/WeatherApp.Domain.csproj", "WeatherApp.Domain/"]
COPY ["WeatherApp.Services/WeatherApp.Services.csproj", "WeatherApp.Services/"]

RUN dotnet restore "WeatherApp/WeatherApp.csproj"
RUN dotnet restore "WeatherApp.Common/WeatherApp.Common.csproj"
RUN dotnet restore "WeatherApp.Domain/WeatherApp.Domain.csproj"
RUN dotnet restore "WeatherApp.Services/WeatherApp.Services.csproj"

COPY . .

WORKDIR "/src/WeatherApp"
RUN dotnet build WeatherApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WeatherApp.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WeatherApp.dll"]