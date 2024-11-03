FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventsCalendar.csproj", "./"]
RUN dotnet restore "EventsCalendar.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "EventsCalendar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventsCalendar.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventsCalendar.dll"]
