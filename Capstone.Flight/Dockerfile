FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5257

ENV ASPNETCORE_URLS=http://+:5257
ENV ASPNETCORE_ENVIRONMENT=Development
# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Capstone.Flight/Capstone.Flight.csproj", "Capstone.Flight/"]
RUN dotnet restore "Capstone.Flight/Capstone.Flight.csproj"
COPY . .
WORKDIR "/src/Capstone.Flight"
RUN dotnet build "Capstone.Flight.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Capstone.Flight.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Capstone.Flight.dll"]
