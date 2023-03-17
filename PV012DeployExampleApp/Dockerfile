#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PV012DeployExampleApp/PV012DeployExampleApp.csproj", "PV012DeployExampleApp/"]
RUN dotnet restore "PV012DeployExampleApp/PV012DeployExampleApp.csproj"
COPY . .
WORKDIR "/src/PV012DeployExampleApp"
RUN dotnet build "PV012DeployExampleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PV012DeployExampleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PV012DeployExampleApp.dll"]