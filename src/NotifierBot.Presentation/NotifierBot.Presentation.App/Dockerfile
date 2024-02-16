FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/NotifierBot.Presentation/NotifierBot.Presentation.App/NotifierBot.Presentation.App.csproj", "src/NotifierBot.Presentation/NotifierBot.Presentation.App/"]
RUN dotnet restore "src/NotifierBot.Presentation/NotifierBot.Presentation.App/NotifierBot.Presentation.App.csproj"
COPY . .
WORKDIR "/src/src/NotifierBot.Presentation/NotifierBot.Presentation.App"
RUN dotnet build "NotifierBot.Presentation.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotifierBot.Presentation.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotifierBot.Presentation.App.dll"]
