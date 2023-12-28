FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ColiseumWebApp/ColiseumWebApp.csproj", "ColiseumWebApp/"]
COPY ["PlayersLib/PlayersLib.csproj", "PlayersLib/"]
COPY ["Strategy/Strategy.csproj", "Strategy/"]
COPY ["CardLib/CardLib.csproj", "CardLib/"]
RUN dotnet restore "ColiseumWebApp/ColiseumWebApp.csproj"
COPY . .
WORKDIR "/src/ColiseumWebApp"
RUN dotnet build "ColiseumWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ColiseumWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ColiseumWebApp.dll"]
