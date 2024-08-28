# Use the official .NET image from the Docker Hub
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8582

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api/Gastos/Gastos.csproj", "Api/Gastos/"]
RUN dotnet restore "Api/Gastos/Gastos.csproj"
COPY . .
WORKDIR "/src/Api/Gastos"
RUN dotnet build "Gastos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gastos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Api/Gastos/appsettings.json /app/
ENTRYPOINT ["dotnet", "Gastos.dll"]
